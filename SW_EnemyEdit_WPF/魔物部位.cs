//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはテンプレートから生成されました。
//
//     このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//     このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SW_EnemyEdit_WPF
{
    using System;
    using System.Collections.Generic;
    
    public partial class 魔物部位
    {
        public int Id { get; set; }
        public int 魔物Id { get; set; }
        public int No { get; set; }
        public bool コア部位 { get; set; }
        public string 部位名 { get; set; }
        public string 攻撃方法 { get; set; }
        public int 命中力 { get; set; }
        public int 打撃点 { get; set; }
        public int 回避力 { get; set; }
        public int 防護点 { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
    
        public virtual 魔物 魔物 { get; set; }
    }
}
