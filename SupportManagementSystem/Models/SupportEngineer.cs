﻿using System;

namespace SupportManagementSystem.Models
{
    public class SupportEngineer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var eng = obj as SupportEngineer;
            if (eng == null)
            {
                return false;
            }

            return Id.Equals(eng.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
