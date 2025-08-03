using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Git_Test
{
    public class clsGit
    {
        private string _ripositoryPath;
        private string[] _commitInfo;
        private string[] _strOlds = new string[3];
        private string[] _strNews = new string[3];
        private string _strCommitID;
        private List<GitHistoryInfo> _lstGitHistoryInfos = new List<GitHistoryInfo>();

        public string RipositoryPath { set { _ripositoryPath = value; } }
        /// <summary>
        /// [0]:コメント内容　[1]:IPアドレス   [2]:判定ステータス
        /// </summary>
        public string[] CommitInfo { set { _commitInfo = value; } }

        public string[] StrOld { get { return _strOlds; } }
        public string[] StrNew { get { return _strNews; } }
        public string CommitID { set { _strCommitID = value; } }
        public List<GitHistoryInfo> LstGitHistoryInfos { get { return _lstGitHistoryInfos; } }

        /// <summary>
        /// リポジトリ作成 戻り値-1:失敗　0:何もしてない 1:リポジトリ作成
        /// </summary>
        /// <returns></returns>
        public int Repository_Init()
        {
            if (!Directory.Exists(_ripositoryPath + @"\.git"))
            {
                string newRepoPath = Repository.Init(_ripositoryPath);

                if (newRepoPath == string.Empty)
                    return -1;
                else
                    return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// コミット実施
        /// </summary>
        /// <returns></returns>
        public bool Repository_Commit()
        {
            //コミットを実施
            using (var repo = new Repository(_ripositoryPath))
            {
                // 全ファイルをステージングする(対象ファイルを選ぶ)
                //Commands.Stage(repo, "*");
                Commands.Stage(repo, @"New\*");
                Commands.Stage(repo, @"Old\*");
                Commands.Stage(repo, "sort.txt");


                // コミット情報を作成
                Signature author = new Signature(_commitInfo[1], _commitInfo[2], DateTime.Now);
                Signature committer = author;

                // リポジトリにコミットを実施
                try
                {
                    Commit commit = repo.Commit(_commitInfo[0], author, committer);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }


        public bool RepositoryList_Get()
        {
            if (_ripositoryPath == null) return false;

            // 修正: using 宣言を通常の using ステートメントに変更
            using (var repo = new Repository(_ripositoryPath))
            {
                // リポジトリのコミット履歴を取得
                foreach (Commit commit in repo.Commits)
                {
                    string[] strSplits = commit.Author.Email.Split('@');
                    string[] strSplits2 = strSplits[1].Split('：');
                    var info = new GitHistoryInfo
                    {
                        CommitId = commit.Sha,
                        TrackingNumber = commit.MessageShort,
                        DAY = commit.Author.When.ToString(),
                        Result = strSplits[0],
                        SubNo = strSplits2[1]
                    };
                    _lstGitHistoryInfos.Add(info);
                    //lstHistory.Items.Add($"Commit_Id:{commit.Sha} - {commit.MessageShort},{commit.Author.Name}<{commit.Author.Email}>,Date:{commit.Author.When}, Message:{commit.Message}");
                }
            }
            return true;
        }

        private IEnumerable<TreeEntry> GetAllTreeEntries(Tree tree)
        {
            foreach (var entry in tree)
            {
                if (entry.TargetType == TreeEntryTargetType.Blob)
                {
                    yield return entry;
                }
                else if (entry.TargetType == TreeEntryTargetType.Tree)
                {
                    foreach (var subEntry in GetAllTreeEntries((Tree)entry.Target))
                    {
                        yield return subEntry;
                    }
                }
            }
        }
        public bool RepositoryHistory_DataGet()
        {
            if (_ripositoryPath == null || _strCommitID == null) return false;

            using (var repo = new Repository(_ripositoryPath))
            {
                // コミットからBlobを取得
                Commit commit = repo.Lookup<Commit>(_strCommitID);
                if (commit != null)
                {
                    // すべてのファイル（サブフォルダ含む）を取得
                    var allEntries = GetAllTreeEntries(commit.Tree).ToList();

                    foreach (var entry in allEntries)
                    {
                        Blob blob = (Blob)entry.Target;
                        string content = blob.GetContentText();
                        switch (entry.Name)
                        {
                            case "OldR.txt":
                                _strOlds[0] = content;
                                break;
                            case "OldM.txt":
                                _strOlds[1] = content;
                                break;
                            case "OldF.txt":
                                _strOlds[2] = content;
                                break;
                            case "NewR.txt":
                                _strNews[0] = content;
                                break;
                            case "NewM.txt":
                                _strNews[1] = content;
                                break;
                            case "NewF.txt":
                                _strNews[2] = content;
                                break;
                            default:
                                // 他のファイルは無視
                                break;
                        }
                        //MessageBox.Show($"ファイル名：{entry.Name} \r\nBlob Content:\n{content}");
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("選択されたコミットが見つかりません");
                    return false;
                }
            }
        }
    }

    public class GitHistoryInfo
    {
        public string TrackingNumber { get; set; }
        public string CommitId { get; set; }
        public string DAY { get; set; }
        public string Result { get; set; }
        public string SubNo { get; set; }
    }
}
