namespace Zearain.AoC23.Domain.AdventDays;

public class DayInput
{
    public string RawInput { get; private set; } = string.Empty;

    public string[] Lines => this.RawInput.Split(Environment.NewLine);

    public string[] Sections => this.RawInput.Split($"{Environment.NewLine}{Environment.NewLine}");
}