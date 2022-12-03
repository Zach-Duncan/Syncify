namespace LearningStarter.Common
{
    public class OptionDto
    {
        public OptionDto(string text, int value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public int Value { get; set; }
    }
}
