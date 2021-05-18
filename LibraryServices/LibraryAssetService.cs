using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

// service layer for application where business logic takes place - like any interaction with the data that's in our database
// and the logic surrounding it will be implemented here.
namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryContext _context;

        public LibraryAssetService(LibraryContext context)
        {
            _context = context;
        }

        // uses the .Add method from the EF DbContext object to
        // add our new item to the correct table
        public void Add(LibraryAsset newAsset)
        {
            _context.Add(newAsset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return 
                _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location);
        }

        public LibraryAsset GetById(int id)
        {
            return 
                _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location)
                .FirstOrDefault(asset => asset.Id == id);
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return GetById(id).Location;
        }

        public string GetDeweyIndex(int id)
        {
            if (_context.Books.Any(book => book.Id == id))
            {
                return 
                    _context.Books
                    .FirstOrDefault(book => book.Id == id)
                    .DeweyIndex;
            } 
            else
            {
                return "";
            }
        }

        public string GetIsbn(int id)
        {
            if (_context.Books.Any(book => book.Id == id))
            {
                return
                    _context.Books
                    .FirstOrDefault(book => book.Id == id)
                    .ISBN;
            }
            else
            {
                return "";
            }
        }

        public string GetTitle(int id)
        {
            return
                _context.LibraryAssets
                .FirstOrDefault(asset => asset.Id == id)
                .Title;
        }

        public string GetType(int id)
        {
            var isBook = _context.LibraryAssets
                .OfType<Book>()
                .Where(asset => asset.Id == id)
                .Any();

            var isVideo = _context.LibraryAssets
                .OfType<Video>()
                .Where(asset => asset.Id == id)
                .Any();

            if (isBook)
                return "Book";

            if (isVideo)
                return "Video";

            return "Unknown Asset Type";
        }

        public string GetAuthorOrDirector(int id)
        {
            if (GetType(id) == "Book")
            {
                return
                    _context.Books
                    .FirstOrDefault(book => book.Id == id)
                    .Author;
            }
            else if (GetType(id) == "Video")
            {
                return
                    _context.Videos
                    .FirstOrDefault(video => video.Id == id)
                    .Director;
            }
            else
            {
                return "couldn't find book or video with id: " + id;
            }
        }
    }
}
