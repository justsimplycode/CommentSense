using GenericLibrary.Database;
using GenericLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using CsvHelper;
using System.Globalization;
using Newtonsoft.Json;

namespace FinalGroupProject.SQLRepository
{
    public class SqlRepository : ISqlRepository
    {
        public ISqlDbConnection DatabaseConnection { get; set; }

        public List<Tag> GetTags()
        {
            List<Tag> tags = new List<Tag>();
            try
            {
                string query = @"select * from tag";

                DatabaseConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query))
                {
                    sqlCommand.Connection = DatabaseConnection.SqlConnectionToDb;

                    SqlDataReader readAllInfo = sqlCommand.ExecuteReader();

                    while (readAllInfo.Read())
                    {
                        Tag tag = new Tag();

                        tag.Id = (int)readAllInfo["id"];
                        tag.Label = (string)readAllInfo["label"];
                        tag.Color = (string)readAllInfo["color"];
                        

                        tags.Add(tag);
                    }
                    readAllInfo.Close();
                }
                DatabaseConnection.Close();
                return tags;
            }
            catch (Exception ex)
            {
                DatabaseConnection.Close();
                throw ex;
            }
        }

        public List<LabelCount> GetLabelCount()
        {
            List<LabelCount> labelCounts = new List<LabelCount>();
            try
            {
                string query = @"select tag.id,tag.label,tag.color,COUNT(*)as labelCount from commentTag_mapping inner join tag on tag.id = commentTag_mapping.tag_id group by tag.id,tag.label,tag.color";

                DatabaseConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query))
                {
                    sqlCommand.Connection = DatabaseConnection.SqlConnectionToDb;

                    SqlDataReader readAllInfo = sqlCommand.ExecuteReader();

                    while (readAllInfo.Read())
                    {
                        LabelCount labelCount = new LabelCount();

                        labelCount.Id = (int)readAllInfo["id"];
                        labelCount.Label = (string)readAllInfo["label"];
                        labelCount.Color = (string)readAllInfo["color"];
                        labelCount.Count = (int)readAllInfo["labelCount"];

                        labelCounts.Add(labelCount);
                    }
                    readAllInfo.Close();
                }
                DatabaseConnection.Close();
                return labelCounts;
            }
            catch (Exception ex)
            {
                DatabaseConnection.Close();
                throw ex;
            }
        }

        public List<Tag> SubQuery(int id)
        {
            try
            {
                string subQuery = $"select tag.id,tag.label,tag.color from commentTag_mapping left join tag on tag.id = commentTag_mapping.tag_id where comment_id = '{id}'";
                List<Tag> tags = new List<Tag>();
                using (SqlCommand sqlCommand = new SqlCommand(subQuery))
                {
                    sqlCommand.Connection = DatabaseConnection.SqlConnectionToDb;
                    SqlDataReader readInfo = sqlCommand.ExecuteReader();

                    while (readInfo.Read())
                    {
                        Tag tag = new Tag();

                        tag.Id = (int)readInfo["id"];
                        tag.Label = (string)readInfo["label"];
                        tag.Color = (string)readInfo["color"];

                        tags.Add(tag);
                    }
                }

                return tags;
            }
            catch (Exception ex)
            {
                DatabaseConnection.Close();
                throw ex;
            }
        }
        public List<CommentTag> GetComments(string orderBy, string checkBox, string userComment, string name, string city, string label,int count, string skip, string top)
        {
            List<CommentTag> comments = new List<CommentTag>();
            try
            {
                bool isTrue = true;
                int counter = Convert.ToInt32(top);
                int countForSkipRow = 0;
                DatabaseConnection.Open();
                while (isTrue)
                {
                    int skipRow = (Convert.ToInt32(skip) + countForSkipRow) * Convert.ToInt32(top);


                    string query = $"select comment.id,comment.name,comment.comment_date,comment.city,comment.user_comment,tag.id from comment left join commentTag_mapping on comment.id = commentTag_mapping.comment_id left join tag on tag.id = commentTag_mapping.tag_id where comment.name like '%{name}%' and comment.city like '%{city}%' and comment.user_comment like '%{userComment}%'";

                    string order = $" order by comment.comment_date {orderBy} offset {skipRow} rows fetch next {top} rows only";

                    if (checkBox == "true")
                    {
                        query = query + $" and tag.id is null" + order;
                    }
                    else if (label == "")
                    {
                        query = query + order;
                    }
                    else
                    {
                        query = query + $" and tag.label in ({label}) group by comment.id,comment.name,comment.comment_date,comment.city,comment.user_comment,tag.id" + order;
                    }
                    

                    using (SqlCommand sqlCommand = new SqlCommand(query))
                    {
                        sqlCommand.Connection = DatabaseConnection.SqlConnectionToDb;

                        SqlDataReader readAllInfo = sqlCommand.ExecuteReader();

                        int previous = 0;
                        bool hasRows = readAllInfo.HasRows;

                        if (!hasRows)
                        {
                            isTrue = false;
                        }
                        while (readAllInfo.Read() && counter > 0)
                        {
                            CommentTag comment = new CommentTag();

                            comment.Id = (int)readAllInfo["id"];
                            if (comment.Id == previous)
                            {
                                continue;
                            }
                            previous = comment.Id;
                            comment.Name = (string)readAllInfo["name"];
                            comment.Date = (DateTime)readAllInfo["comment_date"];
                            comment.City = (string)readAllInfo["city"];
                            comment.UserComment = (string)readAllInfo["user_comment"];



                            List<Tag> tags = SubQuery(comment.Id);



                            comment.Tag = tags;

                            comments.Add(comment);
                            counter--;
                        }
                        readAllInfo.Close();
                    }
                    if(counter <= 0)
                    {
                        isTrue = false;
                    }
                    countForSkipRow += 1;
                }

                DatabaseConnection.Close();
                return comments;
            }
            catch (Exception ex)
            {
                DatabaseConnection.Close();
                throw ex;
            }
        }

        public void PostTagDetail(Tag tag)
        {
            DatabaseConnection.Open();
            SqlTransaction sqlTransaction = DatabaseConnection.SqlConnectionToDb.BeginTransaction();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Transaction = sqlTransaction;

                    sqlCommand.Connection = DatabaseConnection.SqlConnectionToDb;

                    sqlCommand.CommandText = string.Empty;
                    sqlCommand.CommandText = "insert into tag (label,color) values(@Label,@Color)";

                    sqlCommand.Parameters.Add(new SqlParameter("@Label", SqlDbType.NVarChar));
                    sqlCommand.Parameters.Add(new SqlParameter("@Color", SqlDbType.NVarChar));

                    
                    sqlCommand.Parameters["@Label"].Value = tag.Label;
                    sqlCommand.Parameters["@Color"].Value = tag.Color;

                    sqlCommand.ExecuteNonQuery();
                   
                    sqlTransaction.Commit();
                }
                DatabaseConnection.Close();
            }

            catch (Exception ex)
            {
                DatabaseConnection.Close();
                sqlTransaction.Rollback();
                throw ex;
            }
        }

        public void PostCommentDetailsFromCSV()
        {
            string json;
            using (StreamReader reader = new StreamReader(@"C:\DummyDir\FinalGroupProjectData.csv"))
            {
                using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    using (CsvDataReader csvDataReader = new CsvDataReader(csv))
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("Name", typeof(string));
                        dataTable.Columns.Add("Date", typeof(DateTime));
                        dataTable.Columns.Add("City", typeof(string));
                        dataTable.Columns.Add("UserComment", typeof(string));

                        dataTable.Load(csvDataReader);
                        json = JsonConvert.SerializeObject(dataTable);
                    }
                }
            }
            List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(json);

            PostCommentDetails(comments);
        }
        
        public void PostCommentDetails(List<Comment> comments)
        {
            DatabaseConnection.Open();
            SqlTransaction sqlTransaction = DatabaseConnection.SqlConnectionToDb.BeginTransaction();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Transaction = sqlTransaction;

                    sqlCommand.Connection = DatabaseConnection.SqlConnectionToDb;

                    sqlCommand.CommandText = string.Empty;
                    sqlCommand.CommandText = "insert into comment (name,comment_date,city,user_comment) values(@Name,@CommentDate,@City,@UserComment)";

                    sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
                    sqlCommand.Parameters.Add(new SqlParameter("@CommentDate", SqlDbType.Date));
                    sqlCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar));
                    sqlCommand.Parameters.Add(new SqlParameter("@UserComment", SqlDbType.NVarChar));

                    foreach (Comment comment in comments)
                    {
                        sqlCommand.Parameters["@Name"].Value = comment.Name;
                        sqlCommand.Parameters["@CommentDate"].Value = comment.Date;
                        sqlCommand.Parameters["@City"].Value = comment.City;
                        sqlCommand.Parameters["@UserComment"].Value = comment.UserComment;

                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlTransaction.Commit();
                }
                DatabaseConnection.Close();
            }

            catch (Exception ex)
            {
                DatabaseConnection.Close();
                sqlTransaction.Rollback();
                throw ex;
            }
        }

        public void PostCommentTagMapping(CommentTagMapping commentTag)
        {
            DatabaseConnection.Open();
            SqlTransaction sqlTransaction = DatabaseConnection.SqlConnectionToDb.BeginTransaction();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Transaction = sqlTransaction;

                    sqlCommand.Connection = DatabaseConnection.SqlConnectionToDb;

                    sqlCommand.CommandText = string.Empty;
                    sqlCommand.CommandText = "insert into commentTag_mapping (comment_id,tag_id) values(@commentId,@tagId)";

                    sqlCommand.Parameters.Add(new SqlParameter("@commentId", SqlDbType.Int));
                    sqlCommand.Parameters.Add(new SqlParameter("@tagId", SqlDbType.Int));


                    sqlCommand.Parameters["@commentId"].Value = commentTag.CommentId;
                    sqlCommand.Parameters["@tagId"].Value = commentTag.TagId;

                    sqlCommand.ExecuteNonQuery();

                    sqlTransaction.Commit();
                }
                DatabaseConnection.Close();
            }

            catch (Exception ex)
            {
                DatabaseConnection.Close();
                sqlTransaction.Rollback();
                throw ex;
            }
        }
        
        public void DeleteCommentTag(CommentTagMapping commentTag)
        {
            DatabaseConnection.Open();
            SqlTransaction sqlTransaction = DatabaseConnection.SqlConnectionToDb.BeginTransaction();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Transaction = sqlTransaction;

                    sqlCommand.Connection = DatabaseConnection.SqlConnectionToDb;

                    sqlCommand.CommandText = string.Empty;
                    sqlCommand.CommandText = $"delete from commentTag_mapping where comment_id = '{commentTag.CommentId}' and tag_id = '{commentTag.TagId}'";

                    sqlCommand.ExecuteNonQuery();

                    sqlTransaction.Commit();
                }
                DatabaseConnection.Close();
            }

            catch (Exception ex)
            {
                DatabaseConnection.Close();
                sqlTransaction.Rollback();
                throw ex;
            }
        }
    }
}
