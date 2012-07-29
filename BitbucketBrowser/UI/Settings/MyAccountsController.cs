using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using System.Collections.Generic;
using BitbucketSharp.Models;

namespace BitbucketBrowser.UI
{
    public class MyAccountsController : Controller<List<Account>>
    {
        public MyAccountsController()
            : base (false, false)
        {
            Title = "Accounts";
            Model = new List<Account>(Application.Accounts);
            Style = UITableViewStyle.Plain;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Pull the model!

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add, (s, e) => {  
                NavigationController.PushViewController(new LoginViewController(), true);
            });

            NavigationItem.LeftBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Done, (s, e) => {  
                this.DismissModalViewControllerAnimated(true);
            });
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            Refresh(false);
        }

        protected override void OnRefresh ()
        {
            var s = new Section();

            foreach (var account in Application.Accounts)
            {
                var thisAccount = account;
                var t = new StyledElement(thisAccount.Username);
                t.Tapped += () => { 
                    Application.SetUser(thisAccount);
                    this.DismissModalViewControllerAnimated(true);

                };

                s.Add(t);
            };

            Root = new RootElement(Title) { s };
        }

        protected override List<Account> OnUpdate ()
        {
            return Model;
        }

        public override Source CreateSizingSource(bool unevenRows)
        {
            return new EditSource(this);
        }

        private class EditSource : DialogViewController.Source
        {
            public EditSource(DialogViewController dvc) : base (dvc) { }

            public override bool CanEditRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
            {
                return true;
            }

            public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
            {
                return UITableViewCellEditingStyle.Delete;
            }

            public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
            {
                switch (editingStyle)
                {
                    case UITableViewCellEditingStyle.Delete:
                        Console.WriteLine("Deleting!");
                        break;
                }
            }
        }


     
    }
}


