using System;
using System.Text;
using System.Collections.Generic;
using System.Windows;
using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using SummitLog.Services;
using SummitLog.UI.Common;
using SummitLog.UI.DbSettings;
using SummitLog.UI.DbSettings.ViewCommands;
using SummitLog.UI.Main;

namespace SummitLog.Test.UI.DbSettings.ViewCommands
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für DbSettingsViewCommandTests
    /// </summary>
    [TestFixture]
    public class DbSettingsViewCommandTests
    {
        [Test]
        public void TestShowNewWindowWithOwnerParent()
        {
            /* Given : */
            Mock<IDbSettingsView> dbSettingsViewMock = new Mock<IDbSettingsView>();
            dbSettingsViewMock.SetupSet(x => x.DataContext = It.IsAny<IDbSettingsViewModel>());
            dbSettingsViewMock.Setup(x => x.ShowDialog());

            Mock<IDbSettingsViewModel> dbSettingsViewModelMock = new Mock<IDbSettingsViewModel>();
            dbSettingsViewModelMock.Setup(x => x.LoadData());

            Mock<IGenericFactory> genericFactoryMock = new Mock<IGenericFactory>();
            genericFactoryMock.Setup(x => x.Resolve<IDbSettingsView>()).Returns(dbSettingsViewMock.Object);
            genericFactoryMock.Setup(x => x.Resolve<IDbSettingsViewModel>()).Returns(dbSettingsViewModelMock.Object);

            Mock<IWindowParentHelper> windowParentHelperMock = new Mock<IWindowParentHelper>();
            windowParentHelperMock.Setup(x => x.SetOwner<MainView>(dbSettingsViewMock.Object));

            DbSettingsViewCommand dbSettingsViewCommand = new DbSettingsViewCommand(genericFactoryMock.Object, windowParentHelperMock.Object);

            /* When */
            dbSettingsViewCommand.Execute();

            /* Then */
            genericFactoryMock.Verify(x=>x.Resolve<IDbSettingsViewModel>(), Times.Once);
            genericFactoryMock.Verify(x=>x.Resolve<IDbSettingsView>(), Times.Once);
            windowParentHelperMock.Verify(x=>x.SetOwner<MainView>(dbSettingsViewMock.Object), Times.Once);
            dbSettingsViewModelMock.Verify(x=>x.LoadData());
            dbSettingsViewMock.VerifySet(x=>x.DataContext = dbSettingsViewModelMock.Object, Times.Once);
            dbSettingsViewMock.Verify(x=>x.ShowDialog());
        }

    }
}
