namespace Cappuccino.App.iOS.UI.Common;

public class SegmentedControlOptions {
    public string? Title { get; set; }
    public string[]? Options { get; set; }
    public nfloat OriginY { get; set; }
    public nfloat HorizontalPadding { get; set; }
    public nfloat VerticalPadding { get; set; }
    public nfloat Spacing { get; set; }
    public CGSize ContentSize { get; set; }
    public nfloat HeightAmplifier { get; set; }
}