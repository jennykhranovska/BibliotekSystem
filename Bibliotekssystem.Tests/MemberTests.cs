using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bibliotekssystem;
using Xunit;
using LibrarySystem.Core.Models;

namespace Bibliotekssystem.Tests
{

    public class MemberTests
    {
        [Fact]
        public void Constructor_ShouldThrowException_WhenEmailIsEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new Member("M001", "Anna", ""));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new Member("M001", "", "anna@test.com"));
        }
    }
}
