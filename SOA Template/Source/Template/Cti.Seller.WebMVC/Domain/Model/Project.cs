namespace ecrm.Domain.Model
{
    public class Project : BaseDomainModel
    {
        public int ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
    }
}