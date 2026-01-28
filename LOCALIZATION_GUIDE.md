# Fatwa Q&A System - Localization Support (Arabic & English)

## Overview
The Fatwa Q&A System now supports both **English** and **Arabic** with full RTL (Right-to-Left) support for Arabic language display.

## Features

### 🌍 Language Support
- **English (en)** - Default language
- **Arabic (ar)** - Full RTL support

### 🎨 RTL Support
- Automatic text direction handling (LTR for English, RTL for Arabic)
- CSS automatically adjusts margins, padding, and borders for RTL
- Bootstrap classes automatically flip for RTL layout
- Navbar and navigation adjusted for RTL

### 🔄 Language Switching
- Language dropdown in navigation bar
- Language preference persisted in browser cookie
- Automatic redirection to current page after language change
- Smooth language switching experience

## Implementation Details

### Localization Middleware
Located in `Program.cs`:
```csharp
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalization()
    .AddViewLocalization();

var supportedCultures = new[] { "en", "ar" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
```

### Resource Files

#### Location: `Resources/`
```
Resources/
├── SharedResource.en.resx      # Common strings (English)
├── SharedResource.ar.resx      # Common strings (Arabic)
├── SharedResource.cs           # Marker class
├── Views.Home.Index.en.resx    # Home page (English)
├── Views.Home.Index.ar.resx    # Home page (Arabic)
├── Views.Questions.en.resx     # Questions views (English)
├── Views.Questions.ar.resx     # Questions views (Arabic)
├── Views.Admin.en.resx         # Admin views (English)
├── Views.Admin.ar.resx         # Admin views (Arabic)
└── ViewLocalizers.cs           # Marker classes for views
```

#### Resource File Format
Each .resx file contains key-value pairs for translations:
```xml
<data name="KeyName" xml:space="preserve">
  <value>Translated String</value>
</data>
```

### Using Localization in Views

#### Inject the Localizer
```html
@inject IStringLocalizer<SharedResource> SharedLocalizer
@inject IHtmlLocalizer<Views.Home.Index> Localizer
```

#### Use Localized Strings
```html
<!-- Using SharedLocalizer for common strings -->
@SharedLocalizer["AskQuestion"]
@SharedLocalizer["BrowseAnswers"]
@SharedLocalizer["Admin"]

<!-- Using Localizer for page-specific strings -->
@Localizer["WelcomeToFatwaQA"]
@Localizer["EasyToUse"]
```

### Language Detection
- **Default**: English (en)
- **Current Language**: Detected from browser cookie or request header
- **Set Language**: Via language dropdown in navbar
- **Cookie**: `ASPNETCORE_CULTURE` (expires in 1 year)

### RTL CSS Support
Located in `wwwroot/css/site.css`:

```css
html[dir="rtl"] {
  direction: rtl;
  text-align: right;
}

/* Automatic adjustment of common properties */
html[dir="rtl"] .me-2 { margin-right: 0.5rem !important; margin-left: 0 !important; }
html[dir="rtl"] .border-start { border-left: none !important; border-right: 5px solid currentColor !important; }
html[dir="rtl"] .float-start { float: right !important; }
```

### Language Switching Controller
Located in `Controllers/HomeController.cs`:

```csharp
[HttpGet]
public IActionResult SetLanguage(string culture, string returnUrl)
{
    Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
    );
    return LocalRedirect(returnUrl ?? "/");
}
```

### Layout Changes
The `_Layout.cshtml` now includes:
- RTL support via `dir` attribute
- Language dropdown in navigation
- Localized navigation links
- Localized footer content

```html
@{
    var currentCulture = CultureInfo.CurrentCulture.Name;
    var isArabic = currentCulture.StartsWith("ar");
}

<html lang="@currentCulture" dir="@(isArabic ? "rtl" : "ltr")">
```

## Supported Pages & Strings

### Shared Strings (CommonResource)
- Navigation: AskQuestion, BrowseAnswers, Admin
- Buttons: Submit, Cancel, Back, Save, Delete, Edit, Yes, No
- Status: Error, Success, Warning, Info, Loading
- Language: Language, English, Arabic
- Navigation: Home, About, Contact
- Footer: AllRightsReserved

### Home Page
- WelcomeToFatwaQA
- AskIslamicQuestions
- EasyToUse, SimpleAndStraightforward
- ExpertAnswers, AnswersFromScholars
- CompletePrivacy, SubmitAnonymously
- SearchableDatabase, AccessGrowingLibrary
- HowItWorks, AskYourQuestion, OurTeamReviews, GetYourAnswer
- ReadyToGetStarted, SubmitNow

### Questions Page
- SubmitAQuestion, QuestionTitle, QuestionDetails
- Category (with subcategories in both languages)
- YourName, EmailAddress, YourPrivacy
- AnsweredQuestions, BrowseAnsweredQuestions
- NoAnsweredQuestions, BuildingKnowledgeBase
- DidntFindYourAnswer, ReadFullAnswer

### Admin Pages
- AdminPanel, AdminLogin, SecureAccessForAdministrators
- Username, Password, SignIn, RememberThisDevice
- Dashboard, ManageQuestionsAnswers, AdminMenu
- ChangePassword, ManageUsers
- TotalQuestions, PendingReview, QuickActions
- ReviewPending, PublishedAnswers, ManageQuestions
- ReviewAndAnswerQuestion, QuestionDetails, YourAnswer
- AnswerText, ImportantNotice, PublishThisAnswer
- SaveAnswer, QuestionStatus, SubmitterInformation

## Translation Guidelines

### Adding New Translations

1. **Edit .resx files** in `Resources/` folder
2. **Add key-value pairs** for English and Arabic versions
3. **Follow naming convention**: 
   - Page-specific: `Views.ControllerName.Action.[en|ar].resx`
   - Common: `SharedResource.[en|ar].resx`

### Best Practices
- Use descriptive key names (e.g., `QuestionTitle` not `Q1`)
- Keep English and Arabic files in sync
- Test RTL layout with Arabic text
- Use consistent terminology across translations

## Browser Compatibility
- Works with all modern browsers
- Requires cookie support for language persistence
- RTL CSS works in Chrome, Firefox, Safari, Edge

## Performance
- Resource files cached after first load
- Language preference persisted client-side (cookie)
- Minimal performance impact
- No external libraries required (uses built-in ASP.NET Core localization)

## Testing Localization

### Test Steps
1. Navigate to homepage (default: English)
2. Click language dropdown in navbar
3. Select "العربية" (Arabic)
4. Verify:
   - Page content translates
   - RTL layout applies
   - Language persists on page refresh
5. Select "English" to switch back

### Key Things to Verify
- ✅ All text translates correctly
- ✅ Layout adjusts to RTL (navbar, buttons, borders)
- ✅ Form inputs maintain proper direction
- ✅ Images/icons remain in correct position
- ✅ Language preference persists

## Future Enhancements
- Add more languages (French, Spanish, etc.)
- Language-specific email notifications
- Language-based analytics
- Automatic language detection from browser
- Language selection on first visit

## Files Modified for Localization

1. `Program.cs` - Added localization services and middleware
2. `Controllers/HomeController.cs` - Added SetLanguage action
3. `Views/Shared/_Layout.cshtml` - Added language dropdown and RTL support
4. `Views/Home/Index.cshtml` - Added localization injection and strings
5. `wwwroot/css/site.css` - Added RTL CSS support
6. Created `Resources/` folder with all .resx files
7. Created `Resources/SharedResource.cs` - Marker class
8. Created `Resources/ViewLocalizers.cs` - View marker classes

---

**Status**: ✅ Complete - Full Arabic & English Support with RTL  
**Language Support**: 2 (English, Arabic)  
**Last Updated**: January 2026
