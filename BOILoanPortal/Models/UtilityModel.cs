using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOILoanPortal.Models
{
    public class BaseResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
    }


    //public class Rootobject
    //{
    //    public Class1[] Property1 { get; set; }
    //}

    //public class Class1
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public object localGovtArea { get; set; }
    //}



    public class States
    {
        public List<Resp?> allStates { get; set; }
    }

    public class Lgas
    {
        public string? stateName { get; set; }
        public List<Resp?> allLgas { get; set; }
    }


    public class Cities
    {
        public string? lgaName { get; set; }
        public List<Resp?> allCities { get; set; }
    }


    public class Country
    {
        public List<Resp?> allCountries { get; set; }
    }


    //public class Rootobject
    //{
    //    public Allcountry[] allCountries { get; set; }
    //}

    public class Allcountry
    {
        public int id { get; set; }
        public string name { get; set; }
    }




    public class Banks
    {
        public Resp resp { get; set; }
    }

    public class Resp
    {
        public int id { get; set; }
        public string name { get; set; }
    }


    public class InsertInfoResponse
    {
        public string refNumber { get; set; }
        public object companyInformViewModel { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }


    public class InsertInfoFailedResponse
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
        public dynamic errors { get; set; }
    }

    public class Errors
    {
        public string[] JoinedDate { get; set; }
        public string[] CurrentNumberOfEmployees { get; set; }
    }





}
