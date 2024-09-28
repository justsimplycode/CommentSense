using GenericLibrary.Database;
using GenericLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalGroupProject.SQLRepository
{
    public interface ISqlRepository
    {
        /// <summary>
        /// Set connection with database.
        /// </summary>
        ISqlDbConnection DatabaseConnection { get; set; }


        /// <summary>
        ///  Returns all the details of tag like id, label and color for each tag.
        /// </summary>
        /// <returns>Details of tag.</returns>
        List<Tag> GetTags();
        /// <summary>
        ///  Returns all the details of tag like id, label, color and count for each tag.
        /// </summary>
        /// <returns>Label count and details of tag.</returns>
        List<LabelCount> GetLabelCount();
        /// <summary>
        /// Returns all the details of tag like id, label and color for a specific comment.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deatils of tag.</returns>
        List<Tag> SubQuery(int id);
        /// <summary>
        /// Returns all the comments and deatils of tag associated with it according to filters passed.
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="checkBox"></param>
        /// <param name="comment"></param>
        /// <param name="name"></param>
        /// <param name="city"></param>
        /// <param name="label"></param>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <param name="top"></param>
        /// <returns>Details of comment and tags associated with it.</returns>
        List<CommentTag> GetComments(string orderBy,string checkBox,string comment,string name,string city,string label,int count,string skip, string top);

        /// <summary>
        /// Creates a tag in tag table.
        /// </summary>
        /// <param name="tag"></param>
        void PostTagDetail(Tag tag);
        /// <summary>
        /// Inserts data from a CSV file to the database.
        /// </summary>
        void PostCommentDetailsFromCSV();
        /// <summary>
        /// Inserts a comment detail in the database.
        /// </summary>
        /// <param name="comments"></param>
        void PostCommentDetails(List<Comment> comments);
        /// <summary>
        /// Creates a mapping between comment and tags.
        /// </summary>
        /// <param name="commentTag"></param>
        void PostCommentTagMapping(CommentTagMapping commentTag);

        void DeleteCommentTag(CommentTagMapping commentTag);

    }
}
