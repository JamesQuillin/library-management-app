using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
    // define interface for any service that implements this 
    // interface -- starting with methods to return all objects
    // of this particular type, individual instances of objects 
    // by their id, and ability to add a new item of this type

    // can also add other methods depending on what we need in 
    // in our service layer as we go
    public interface ILibraryAsset
    {
        IEnumerable<LibraryAsset> GetAll();
        LibraryAsset GetById(int id);
        void Add(LibraryAsset newAsset);
        string GetAuthorOrDirector(int id);
        string GetDeweyIndex(int id);
        string GetType(int id);
        string GetTitle(int id);
        string GetIsbn(int id);

        LibraryBranch GetCurrentLocation(int id);
    }
}
