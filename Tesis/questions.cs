//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tesis
{
    using System.Collections.Generic;
    
    public partial class questions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public questions()
        {
            this.questionoptions = new HashSet<questionoptions>();
            this.evaluations = new HashSet<evaluations>();
        }
    
        public System.Guid Id { get; set; }
        public string QuestionText { get; set; }
        public string ImagePath { get; set; }
        public int Score { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<questionoptions> questionoptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<evaluations> evaluations { get; set; }
    }
}
