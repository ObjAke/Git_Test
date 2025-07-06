using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibGit2Sharp;


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
            string ripoPath = txtRipoPath.Text;
            // 修正: using 宣言を通常の using ステートメントに変更
            using (var repo = new Repository(ripoPath))
            {
                // リストボックスにコミット履歴を表示
                lstHistory.Items.Clear();
                // リポジトリのコミット履歴を取得
                foreach (Commit commit in repo.Commits)
                {
                    lstHistory.Items.Add($"Commit_Id:{commit.Sha} - {commit.MessageShort},{commit.Author.Name}<{commit.Author.Email}>,Date:{commit.Author.When}, Message:{commit.Message}");
                }
            }
        }

        private void btnBlobGet_Click(object sender, EventArgs e)
        {
            if (lstHistory.SelectedItem == null)
            {
                MessageBox.Show("コミットを選択してください");
                return;
            }

            using(var repo = new Repository(txtRipoPath.Text))
            {
                // 選択されたコミットのSHAを取得
                string selectedCommit = lstHistory.SelectedItem.ToString();
                string commitSha = selectedCommit.Split('-')[0].Replace("Commit_Id:", "").Trim();
                // コミットからBlobを取得
                Commit commit = repo.Lookup<Commit>(commitSha);
                if (commit != null)
                {
                    foreach (var entry in commit.Tree)
                    {
                        if (entry.TargetType == TreeEntryTargetType.Blob)
                        {
                            Blob blob = (Blob)entry.Target;
                            string content = blob.GetContentText();
                            MessageBox.Show($"ファイル名：{entry.Name} \r\nBlob Content:\n{content}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("選択されたコミットが見つかりません");
                }
            }
        }
    }
}
