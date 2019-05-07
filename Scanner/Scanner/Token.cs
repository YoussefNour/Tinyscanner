namespace Scanner
{
    class Token
    {
        public enum tokentypes{reserved_word,special_symbol,identifier,number,comment};
        private string val;
        private tokentypes type; 

        public Token(string val,tokentypes type)
        {
            this.val = val;
            this.type = type;
        }
        public Token()
        {

        }
        public void setval(string value)
        {
            val = value;
        }
        public void settype(tokentypes type)
        {
            this.type = type;
        }
        public string getval()
        {
            return val;
        }
    }
}
