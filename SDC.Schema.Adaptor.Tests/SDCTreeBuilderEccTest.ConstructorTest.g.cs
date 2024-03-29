using System.Data;
using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDC.DAL.DataSets;
using SDC;
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace SDC.Tests
{
    public partial class SDCTreeBuilderEccTest
    {

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException31()
{
    SDCTreeBuilderEcc sDCTreeBuilderEcc;
    sDCTreeBuilderEcc =
      this.ConstructorTest((string)null, (IFormDesignDataSets)null, (string)null);
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]

public void ConstructorTestThrowsNullReferenceException126()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets((string)null);
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NoNullAllowedException))]
public void ConstructorTestThrowsNoNullAllowedException366()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException392()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\0");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException143()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\0");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ("\0", (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(ArgumentException))]
public void ConstructorTestThrowsArgumentException342()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("뎚");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException108()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\0");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc =
        this.ConstructorTest("", (IFormDesignDataSets)formDesignDataSets, "\0");
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException633()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\0");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ("0", (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(ArgumentException))]
public void ConstructorTestThrowsArgumentException200()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(ArgumentException))]
public void ConstructorTestThrowsArgumentException983()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("떡剺");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException46()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\0");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc =
        this.ConstructorTest("-", (IFormDesignDataSets)formDesignDataSets, "\0");
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException417()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\0");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ("0\t", (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(ArgumentException))]
public void ConstructorTestThrowsArgumentException898()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets(";⮷༗");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException942()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\0");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              (":\t", (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(ArgumentException))]
public void ConstructorTestThrowsArgumentException644()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("䞾=");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(ArgumentException))]
public void ConstructorTestThrowsArgumentException715()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\tȤדּ");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(ArgumentException))]
public void ConstructorTestThrowsArgumentException42()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("䬴");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException129()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\0");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ("-\0", (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException714()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\n;");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException989()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\t");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException39201()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\0");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(ArgumentException))]
public void ConstructorTestThrowsArgumentException182()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\0馯");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}

[TestMethod]
[PexGeneratedBy(typeof(SDCTreeBuilderEccTest))]
[PexRaisedException(typeof(ArgumentException))]
public void ConstructorTestThrowsArgumentException330()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      FormDesignDataSets formDesignDataSets;
      SDCTreeBuilderEcc sDCTreeBuilderEcc;
      formDesignDataSets = new FormDesignDataSets("\0\0");
      disposables.Add((IDisposable)formDesignDataSets);
      sDCTreeBuilderEcc = this.ConstructorTest
                              ((string)null, (IFormDesignDataSets)formDesignDataSets, (string)null);
      disposables.Dispose();
    }
}
    }
}
