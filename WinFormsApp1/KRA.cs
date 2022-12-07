namespace WinFormsApp1
{
    public class KRA
    {
        private List<double> _term1;
        private List<double> _term2;
        private List<double> _term3;

        public KRA() { }

        public void setValues(List<double> t1, List<double> t2, List<double> t3) {
            this._term1 = t1; this._term2 = t2; this._term3 = t3;
        }
    }
}
