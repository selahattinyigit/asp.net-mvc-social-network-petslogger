//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace petslogger.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_post()
        {
            this.tb_comment = new HashSet<tb_comment>();
            this.tb_like = new HashSet<tb_like>();
        }
    
        public int id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string text { get; set; }
        public string post_url { get; set; }
        public Nullable<System.DateTime> since { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_comment> tb_comment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_like> tb_like { get; set; }
        public virtual tb_user tb_user { get; set; }
    }
}