using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;

namespace Com.Gosol.CMS.Web
{
    [TestFixture]
    public class UnitTest
    {
        [SetUp]
        public void Init()
        {            
        }

        [Test]
        public void TransferFunds()
        {            
            Assert.AreEqual(4, 2*2);            
        }
    }
}