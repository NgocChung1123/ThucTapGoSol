using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Utility;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Com.Gosol.CMS.Web
{
    public static class PagingHelper
    {
        public static void CreatePaging(int totalRow, int currentPage, ref PlaceHolder pageControl)
        {
            int PageSize = IdentityHelper.GetPageSize();

            int pageCount = (totalRow / PageSize);
            if (totalRow % PageSize != 0) pageCount++;
            if (pageCount > 1 && pageCount < 10)
            {
                for (int i = 0; i < pageCount; i++)
                {
                    if (i == currentPage - 1)
                    {
                        Label lblPage = new Label();
                        lblPage.Text = (i + 1).ToString();
                        lblPage.CssClass = "current";
                        pageControl.Controls.Add(lblPage);
                    }
                    else
                    {
                        HyperLink hplPage = new HyperLink();
                        Uri pageUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                        hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString();
                        hplPage.Text = (i + 1).ToString();
                        pageControl.Controls.Add(hplPage);
                    }
                }
            }
            else if (pageCount >= 10)
            {
                if (currentPage - 5 > 0 && currentPage + 4 >= pageCount)
                {
                    HyperLink firstPage = new HyperLink();
                    Uri pageUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                    firstPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=1";
                    firstPage.Text = "Trang đầu";
                    firstPage.CssClass = "firstPage";
                    pageControl.Controls.Add(firstPage);

                    for (int i = currentPage - 5; i < pageCount; i++)
                    {
                        if (i == currentPage - 1)
                        {
                            Label lblPage = new Label();
                            lblPage.Text = (i + 1).ToString();
                            lblPage.CssClass = "current";
                            pageControl.Controls.Add(lblPage);
                        }
                        else
                        {
                            HyperLink hplPage = new HyperLink();
                            hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString();
                            hplPage.Text = (i + 1).ToString();
                            pageControl.Controls.Add(hplPage);
                        }
                    }
                }
                else if (currentPage - 5 > 0 && currentPage + 4 < pageCount)
                {
                    HyperLink firstPage = new HyperLink();
                    Uri pageUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                    firstPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=1";
                    firstPage.Text = "Trang đầu";
                    firstPage.CssClass = "firstPage";
                    pageControl.Controls.Add(firstPage);

                    for (int i = currentPage - 5; i < currentPage + 4; i++)
                    {
                        if (i == currentPage - 1)
                        {
                            Label lblPage = new Label();
                            lblPage.Text = (i + 1).ToString();
                            lblPage.CssClass = "current";
                            pageControl.Controls.Add(lblPage);
                        }
                        else
                        {
                            HyperLink hplPage = new HyperLink();
                            hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString();
                            hplPage.Text = (i + 1).ToString();
                            pageControl.Controls.Add(hplPage);
                        }
                    }

                    HyperLink lastPage = new HyperLink();
                    lastPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + pageCount.ToString();
                    lastPage.Text = "Trang cuối";
                    lastPage.CssClass = "lastPage";
                    pageControl.Controls.Add(lastPage);
                }
                else if (currentPage - 5 <= 0 && currentPage + 4 < pageCount)
                {
                    Uri pageUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);

                    for (int i = 0; i < currentPage + 4; i++)
                    {
                        if (i == currentPage - 1)
                        {
                            Label lblPage = new Label();
                            lblPage.Text = (i + 1).ToString();
                            lblPage.CssClass = "current";
                            pageControl.Controls.Add(lblPage);
                        }
                        else
                        {
                            HyperLink hplPage = new HyperLink();
                            hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString();
                            hplPage.Text = (i + 1).ToString();
                            pageControl.Controls.Add(hplPage);
                        }
                    }

                    HyperLink lastPage = new HyperLink();
                    lastPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + pageCount.ToString();
                    lastPage.Text = "Trang cuối";
                    lastPage.CssClass = "lastPage";
                    pageControl.Controls.Add(lastPage);
                }
            }
        }
    }
}