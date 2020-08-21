using System;
namespace ef_core_sqlite_example.Model
{
    public class Medium
    {        
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Kind { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string Autor { get; set; }
    }
}
