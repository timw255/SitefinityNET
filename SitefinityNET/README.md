# Sitefinity.NET - .NET REST Client for Telerik Sitefinity CMS

**_Got an idea or better way to do something? I welcome pull requests. Please send them. :)_**

Sitefinity.NET is a C# client that adds Sitefinity connectivity to third-party applications.

#### What works

* Querying blogs, blog posts, albums, images and news items
* Creating new blogs, blog posts, albums, images and news items

#### What might work

I wrote methods for a bunch of stuff but haven't tested them yet.

* Deleting content
* Batch deleting content
* Get and Set ratings of content
* Batch publishing and un-publishing content
* Deleting temp versions of content

#### Before we go any further...

This is a public release of a "proof of concept". It's meant to get the ball rolling, so to speak.
What I'm saying here is don't blame me if you blow up your site with it.

If you're OK with that and ready for some fun. Here we go!

#### Authentication (where it all begins...)

Create an instance of `SitefinityClient` and authenticate.

```csharp
using SitefinityNET.Managers.GenericContent;
using SitefinityNET.Sitefinity.Models.GenericContent;

SitefinityClient sf = new SitefinityClient("admin", "password", "http://localhost");

sf.SignIn();
```

#### Managers

Managers are the worker bees of this operation.

Something to note: Currently, methods that begin with 'List' return 'List<dynamic>' so if you don't know exactly what property you're looking for, some debugging may be necessary.

```csharp
LibrariesManager manager = new LibrariesManager(sf);

// This is how we build the list of filters to include
Dictionary<Utility.ContentFilter, string> filters = new Dictionary<Utility.ContentFilter, string>();
filters.Add(Utility.ContentFilter.TitleLike, "Default"); // Other options include: TitleEquals, IdEquals

// For convienience
string filterString = Utility.BuildFilterString(filters);

var items = manager.ListAlbums(filterString, 0, 50);

var newItem = new Image();

string newItemTitle = "New Image Title";

newItem.Title = newItemTitle;
newItem.UrlName = Regex.Replace(newItemTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

Guid parentId = items.First().Id;

Image item = manager.CreateImage(newItem, parentId, false);
```

#### Uploading Binary Content

Upload binary content (images, document, etc.) by using the `UploadContent` method of the manager you're working with.

This example pulls the image from a remote server but the data could come from a database or local filesystem if it needed to.

```csharp
if (item.Id != Guid.Empty)
{
	string imageUrl = "http://www.codemoar.com/images/default-source/demo/sitefinity-logo.png";
	
	using (var webClient = new WebClient())
	{
		byte[] imageBytes = webClient.DownloadData(someUrl);

		// Upload the file and tell Sitefinity which item it belongs to (via item.Id)
		manager.UploadContent(parentId, item.Id, imageBytes, "sitefinity-logo.png", "image/png");
	}
}
```

#### Signing Out (because it's polite...)

Make sure to sign out when your done.

```csharp
sf.SignOut();
```

#### Support for Custom Fields

Custom field values can be set on content. The fields must have already been created in Sitefinity for the values to stick.

```csharp
var newItem = new Blog();

string newItemTitle = "New Blog";

newItem.Title = newItemTitle;
newItem.UrlName = Regex.Replace(newSingleItemTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

newItem.AddProperty("VeryCustomShortTextField", "Some Value");
newItem.AddProperty("VeryCustomNumberField", 42);
newItem.AddProperty("VeryCustomYesNoField", true);

var returnedItem = manager.CreateBlog(newItem);
```

It's possible to check for values when content is recieved from Sitefinity.

```csharp
Guid itemId = Guid.Parse("8f990bfd-13ef-6fb1-b1e0-ff0000cacdaa");

var item = manager.GetBlog(itemId);

if (item.HasProperty("VeryCustomShortTextField"))
{
	var fieldValue = item.GetValue<string>("VeryCustomShortTextField");
}

item.SetValue("VeryCustomShortTextField", "Some Other Value");
```