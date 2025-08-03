using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace Git_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ListViewのViewをDetailsに設定
            listViewHistory.View = View.Details;

            // カラムが未追加の場合のみ追加
            if (listViewHistory.Columns.Count == 0)
            {
                listViewHistory.Columns.Add("CommitID", 120);
                listViewHistory.Columns.Add("DAY", 80);
                listViewHistory.Columns.Add("TrackingNumber", 120);
                listViewHistory.Columns.Add("SubNo", 80);
                listViewHistory.Columns.Add("Result", 120);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newRepoPath = Repository.Init(txtRipoPath.Text);
            using (var repo = new Repository(txtRipoPath.Text))
            {
                Commands.Stage(repo, "*"); // 全てのファイルをステージング
                                           // コミット情報を作成
                Signature author = new Signature("sakai", "@jugglingnutcase", DateTime.Now);
                Signature committer = author;

                // リポジトリにコミットを実施
                Commit commit = repo.Commit("初回打上", author, committer);
            }
            ;

            MessageBox.Show(newRepoPath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string gitPath = txtRipoPath.Text;
            string strComment = txtChangeContent.Text;
            if (strComment.Length < 3)
            {
                MessageBox.Show("変更内容のコメントは３文字以上必要です");
                return;
            }

            //コミットを実施
            using (var repo = new Repository(gitPath))
            {
                //テキストファイルを作成(トライ敵に実施するため)
                //var content = "Commit this!";
                //File.WriteAllText(Path.Combine(repo.Info.WorkingDirectory, gitPath + @"\fileToCommit.txt"), content);

                // 変更されたファイルを全て取得
                var changes = repo.Diff.Compare<TreeChanges>(repo.Head.Tip.Tree, DiffTargets.WorkingDirectory);

                if (changes.Count > 0)
                {
                    // 変更ファイルをすべてステージング
                    foreach (var change in changes)
                    {
                        repo.Index.Add(change.Path);
                    }
                    repo.Index.Write();

                    // コミット情報を作成
                    Signature author = new Signature("sakai", "@jugglingnutcase", DateTime.Now);
                    Signature committer = author;

                    // リポジトリにコミットを実施
                    Commit commit = repo.Commit(strComment, author, committer);
                } else
                {
                    MessageBox.Show("変更されたファイルがありません");
                }
            }
        }

        private void btnHistoryRead_Click(object sender, EventArgs e)
        {
            //string ripoPath = txtRipoPath.Text;
            //// 修正: using 宣言を通常の using ステートメントに変更
            //using (var repo = new Repository(ripoPath))
            //{
            //    // リストボックスにコミット履歴を表示
            //    lstHistory.Items.Clear();
            //    // リポジトリのコミット履歴を取得
            //    foreach (Commit commit in repo.Commits)
            //    {
            //        lstHistory.Items.Add($"Commit_Id:{commit.Sha} - {commit.MessageShort},{commit.Author.Name}<{commit.Author.Email}>,Date:{commit.Author.When}, Message:{commit.Message}");
            //    }
            //}
            clsGit clsGit = new clsGit();
            clsGit.RipositoryPath = txtRipoPath.Text;
            bool flg = clsGit.RepositoryList_Get();
            if (flg)
            {
                listViewHistory.Items.Clear();

                // DAYが文字列の場合はDateTimeに変換して昇順ソート
                var sortedList = clsGit.LstGitHistoryInfos
                    .OrderBy(x => DateTime.Parse(x.DAY))
                    .ToList();

                foreach (var item in clsGit.LstGitHistoryInfos)
                {
                    listViewHistory.Items.Add(new ListViewItem(new string[]
                    {
                        item.CommitId,
                        item.DAY,
                        item.TrackingNumber,
                        item.SubNo,
                        item.Result
                        
                    }));
                    //lstHistory.Items.Add($"Commit_Id:{item.CommitId} - {item.Result}, {item.DAY}, {item.TrackingNumber}, {item.SubNo}");
                }
            }
            else
            {
                MessageBox.Show("コミット履歴の取得に失敗しました");
            }
        }

        private void btnBlobGet_Click(object sender, EventArgs e)
        {
            if (listViewHistory.SelectedItems == null)
            {
                MessageBox.Show("コミットを選択してください");
                return;
            }

            using(var repo = new Repository(txtRipoPath.Text))
            {
                // 選択されたコミットのSHAを取得
                string commitSha = listViewHistory.SelectedItems[0].Text;
                clsGit clsGit = new clsGit();
                clsGit.RipositoryPath = txtRipoPath.Text;
                clsGit.CommitID = commitSha;        
                bool flg = clsGit.RepositoryHistory_DataGet();
                if (!flg)
                {
                    MessageBox.Show("Blobの取得に失敗しました");
                    return;
                }
                string[] strOlds = clsGit.StrOld;
                string[] strNews = clsGit.StrNew;

                textBox1.Text = strOlds[0].Replace("\n","\r\n");
                textBox2.Text = strOlds[1].Replace("\n", "\r\n");
                textBox3.Text = strOlds[2].Replace("\n", "\r\n");
                textBox4.Text = strNews[0].Replace("\n", "\r\n");
                textBox5.Text = strNews[1].Replace("\n", "\r\n");
                textBox6.Text = strNews[2].Replace("\n", "\r\n");
            }
        }

        private void listViewHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
