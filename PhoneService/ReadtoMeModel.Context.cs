﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhoneService
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    //using System.Data.Objects;
    //using System.Data.Objects.DataClasses;

    using System.Linq;
    
    public partial class ISTORE2Entities : DbContext
    {
        public ISTORE2Entities()
            : base("name=ISTORE2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int FileContentInsert(string title, string content, string url, string file, string userID, Nullable<int> speechRate, Nullable<int> voiceID)
        {
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            var contentParameter = content != null ?
                new ObjectParameter("Content", content) :
                new ObjectParameter("Content", typeof(string));
    
            var urlParameter = url != null ?
                new ObjectParameter("url", url) :
                new ObjectParameter("url", typeof(string));
    
            var fileParameter = file != null ?
                new ObjectParameter("File", file) :
                new ObjectParameter("File", typeof(string));
    
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var speechRateParameter = speechRate.HasValue ?
                new ObjectParameter("SpeechRate", speechRate) :
                new ObjectParameter("SpeechRate", typeof(int));
    
            var voiceIDParameter = voiceID.HasValue ?
                new ObjectParameter("VoiceID", voiceID) :
                new ObjectParameter("VoiceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FileContentInsert", titleParameter, contentParameter, urlParameter, fileParameter, userIDParameter, speechRateParameter, voiceIDParameter);
        }
    
        public virtual ObjectResult<FileContentMyCollSelectAll_Result> FileContentMyCollSelectAll(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FileContentMyCollSelectAll_Result>("FileContentMyCollSelectAll", userIDParameter);
        }
    
        public virtual int FileContentUpdateStatus(string status, Nullable<int> fileContenetID)
        {
            var statusParameter = status != null ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(string));
    
            var fileContenetIDParameter = fileContenetID.HasValue ?
                new ObjectParameter("FileContenetID", fileContenetID) :
                new ObjectParameter("FileContenetID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FileContentUpdateStatus", statusParameter, fileContenetIDParameter);
        }
    
        public virtual ObjectResult<GetFileContentNew_Result> GetFileContentNew()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetFileContentNew_Result>("GetFileContentNew");
        }
    
        public virtual ObjectResult<GetVoices_Result> GetVoices()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetVoices_Result>("GetVoices");
        }
    }
}
