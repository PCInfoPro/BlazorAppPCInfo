using Microsoft.Extensions.Localization;

public class SharedLocalizer
{
    private readonly IStringLocalizer _localizer;

    public SharedLocalizer(IStringLocalizer<SharedLocalizer> localizer)
    {
        _localizer = localizer;
    }

    public LocalizedString this[string key] => _localizer[key];
    public LocalizedString this[string key, params object[] arguments] => _localizer[key, arguments];
}
