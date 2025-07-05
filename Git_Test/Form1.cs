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
            string newRepoPath = Repository.Init(@"C:\Users\massa\Documents\Test");

            MessageBox.Show(newRepoPath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string gitPath = @"C:\Users\massa\Documents\Test";
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

                // ファイルをステージングする(対象ファイルを選ぶ)
                repo.Index.Add("fileToCommit.txt");
                repo.Index.Add("Git_test.txt");
                repo.Index.Write();

                // コミット情報を作成
                Signature author = new Signature("sakai", "@jugglingnutcase", DateTime.Now);
                Signature committer = author;

                // リポジトリにコミットを実施
                Commit commit = repo.Commit(strComment, author, committer);
            }
        }
    }
}
