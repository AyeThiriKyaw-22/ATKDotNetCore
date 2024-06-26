﻿using ATKDotNetCore.Shared;
using ATKDotNetCore.WinFormsApp.Models;
using ATKDotNetCore.WinFormsApp.Queries;

namespace ATKDotNetCore.WinFormsApp;

public partial class FrmBlogList : Form
{
    private readonly DapperService _dapperService;
    //private const int _edit = 1;
    //private const int _delete = 2;

    public FrmBlogList()
    {
        InitializeComponent();
        dgvData.AutoGenerateColumns = false;
        _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
    }

    private void FrmBlogList_Load(object sender, EventArgs e)
    {
        BlogList();
    }

    private void BlogList()
    {
        List<BlogModel> lst = _dapperService.Query<BlogModel>(BlogQuery.BlogListQuery);
        dgvData.DataSource = lst;
    }

    private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        //int columnIndex = e.ColumnIndex;
        //int rowIndex = e.RowIndex;

        if (e.RowIndex == -1) return;

        #region If Case

        var blogId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colId"].Value);

        if (e.ColumnIndex == (int)EnumFormControlType.Edit)
        {
            FrmBlog frm = new FrmBlog(blogId);
            frm.ShowDialog();

            BlogList();
        }
        else if (e.ColumnIndex == (int)EnumFormControlType.Delete)
        {
            var dialogResult = MessageBox.Show("Are you sure to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult != DialogResult.Yes) return;

            DeleteBlog(blogId);
        }

        #endregion

        #region Switch Case

        int index = e.ColumnIndex;
        EnumFormControlType enumFormControlType = (EnumFormControlType)index;
        switch (enumFormControlType)
        {
            case EnumFormControlType.Edit:
                FrmBlog frm = new FrmBlog(blogId);
                frm.ShowDialog();

                BlogList();
                break;
            case EnumFormControlType.Delete:
                var dialogResult = MessageBox.Show("Are you sure to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes) return;

                DeleteBlog(blogId);
                break;
            case EnumFormControlType.None:
            default:
                break;
        }

        #endregion

    }

    private void DeleteBlog(int id)
    {
        string query = @"DELETE FROM[dbo].[Tbl_Blog]
        WHERE BlogId = @BlogId";

        var result = _dapperService.Execute(query, new BlogModel() { BlogId = id });
        string message = result > 0 ? "Deleting successful." : "Deleting failed.";
        MessageBox.Show(message);
    }
}
