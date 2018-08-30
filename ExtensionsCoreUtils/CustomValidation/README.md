# ExtensionsCoreUtils
---

## Introduction

In web applications, the best practices saids that the business logic needs to be allocated and excuted in server side, but for a correct UxD you need to avoid unnecesaries request, so you need to repeat the same validations in cliente side for usability. Don´t repeat yourself making validations. With this new feature,
you can convert yours DTOs into a metadata´s JSON. For why? You can offer the metadata in a simple Rest service and the client side after consume, the only to have to do is transform the metadata into the correct form and execute. This way, the client side don´t have any business logic and you only need one part of maintenance.   

## Features
1. Add ScanValidations method in IServiceCollection class for use in start application. 
2. Add ValidationList method in IServiceCollection class for get a dictionary. 
3. CustomValidationAttribute, with the parameters for execute javascript code and/or references functions of javascript libraries.
4. DomainValidationNameAttribute, set a custom name or by default the class name and indicates that this class must be scanned.
5. Execute javascript code.  

#### **Model Reflection**

##### Usage
Add attrutes in your DTOs.
```csharp
namespace Test.Domain
{
    [DomainValidationName("AlbumTEST1")]
    public class Album
    {
        [ScaffoldColumn(false)]
        public int AlbumId { get; set; }
        [DisplayName("Genre")]
        public int GenreId { get; set; }
        [DisplayName("Artist")]
        public int ArtistId { get; set; }
        [Required(ErrorMessage = "An Album Title is required")]
        [StringLength(160)]
        public string Title { get; set; }
        [Range(0.01, 100.00,
            ErrorMessage = "Price must be between 0.01 and 100.00")]
        public decimal Price { get; set; }
        [DisplayName("Album Art URL")]
        [StringLength(1024)]
        public string AlbumArtUrl { get; set; }
        [Attributes.CustomValidation(ErrorMessage = "ERROR_RESOURCES_TEST", FunctionValidation = "function(value){ return true;}", FunctionName = "foo")]
        [Attributes.CustomValidation(ErrorMessage = "bar_message", FunctionValidation = " function (value) { return value != null; }")]
        public virtual StudentDto student { get; set; }
    }
}
```
Scan
```csharp
namespace MyApp
{
    public class Startup
    {
        ......

        public Dictionary<string, string> MyValidations { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ......
            //Add this line in your configuration services.
            services.ScanValidations("Myspace.Domain");
            ......
            MyValidations =  service.ValidationList();
         }
  ......
}
```
Result: validations and types.
````json
Key = AlbumTEST1, 
Value = {"album": {
  "albumId": [
    {
      "validations": [
        {
          "scaffoldcolumn": {
            "scaffold": false
          }
        }
      ]
    },
    {
      "type": "int32"
    }
  ],
  "genreId": [
    {
      "validations": [
        {
          "displayname": {}
        }
      ]
    },
    {
      "type": "int32"
    }
  ],
  "artistId": [
    {
      "validations": [
        {
          "displayname": {}
        }
      ]
    },
    {
      "type": "int32"
    }
  ],
  "title": [
    {
      "validations": [
        {
          "required": {
            "allowEmptyStrings": false,
            "requiresValidationContext": false
          }
        },
        {
          "stringlength": {
            "maximumLength": 160,
            "minimumLength": 0,
            "requiresValidationContext": false
          }
        }
      ]
    },
    {
      "type": "string"
    }
  ],
  "price": [
    {
      "validations": [
        {
          "range": {
            "parseLimitsInInvariantCulture": false,
            "convertValueInInvariantCulture": false,
            "requiresValidationContext": false
          }
        }
      ]
    },
    {
      "type": "decimal"
    }
  ],
  "albumArtUrl": [
    {
      "validations": [
        {
          "displayname": {}
        },
        {
          "stringlength": {
            "maximumLength": 1024,
            "minimumLength": 0,
            "requiresValidationContext": false
          }
        }
      ]
    },
    {
      "type": "string"
    }
  ],
  "student": [
    {
      "validations": [
        {
          "customvalidation": {
            "fn": "function(value){ return true;}",
            "fnName": "foo",
            "message": "ERROR_RESOURCES_TEST"
          }
        },
        {
          "customvalidation": {
            "fn": " function (value) { return value != null; }",
            "fnName": null,
            "message": "bar_message"
          }
        }
      ]
    },
    {
      "type": "studentDto"
    }
  ]
}}
````
## References

https://github.com/pauldotknopf/vroomjs-core