﻿using System;
using System.Collections.Generic;
using SQLite;

namespace SQLite_
{
    //テーブル名を指定
    [Table("User")]
    public class UserModel
    {
        //プライマリキー　自動採番されます
        [PrimaryKey, AutoIncrement, Column("_id")]
        //idカラム
        public int Id { get; set; }
        //名前カラム
        public string Name { get; set; }

        //Userテーブルに行追加するメソッドです
        //Insert文的なの
        public static void insertUser(string name)
        {
            //データベースに接続
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {

                try
                {
                    //データベースにUserテーブルを作成します
                    db.CreateTable<UserModel>(); //エラーが出るから<UserModel>付けた
                                                 //これでなぜか動く!

                    //Userテーブルに行追加します
                    db.Insert(new UserModel() { Name = name });

                    db.Commit();

                }
                catch (Exception e)
                {

                    db.Rollback();
                    System.Diagnostics.Debug.WriteLine(e);

                }
            }
        }

        //Userテーブルのuserを削除するメソッド
        //delete文的なの
        public static void deleteUser(string name)
        {

            //データベースに接続
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {
                //db.BeginTransaction();  //このサイト https://qiita.com/alzybaad/items/9356b5a651603a548278
                try
                {
                    db.CreateTable<UserModel>();

                    db.Delete(name);
                }
                catch (Exception e)
                {
                    db.Rollback();
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }

        }


        //Userテーブルの行データを取得します
        //select文的なの
        public static List<UserModel> selectUser() //エラーが出るから<UserModel>付けた
        {
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {

                try
                {
                    //データベースに指定したSQLを発行します
                    return db.Query<UserModel>("SELECT * FROM [User] "); //エラーが出るから<UserModel>付けた

                }
                catch (Exception e)
                {

                    System.Diagnostics.Debug.WriteLine(e);
                    return null;
                }
            }
        }
    }
}
