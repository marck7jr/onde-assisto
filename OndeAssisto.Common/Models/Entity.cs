using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OndeAssisto.Common.Models
{
    public abstract class Entity : ObservableObject
    {
        private Guid guid;
        private DateTime createdAt;
        private DateTime updatedAt;

        [Key]
        public Guid Guid { get => guid; set => Set(ref guid, value); }
        public DateTime CreatedAt { get => createdAt; set => Set(ref createdAt, value); }
        public DateTime UpdatedAt { get => updatedAt; set => Set(ref updatedAt, value); }

    }
}
