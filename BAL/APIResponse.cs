using System;

namespace BAL
{
    public class APIResponse
    {
        public object response { get; set; }
        public bool hasErrors { get; set; }
        public string errors { get; set; }
        public string errorCode { get; set; }
        //public string VoipCallNo { get; set; }
    }
}
