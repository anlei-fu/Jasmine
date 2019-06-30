namespace Formatter
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_change_input = new System.Windows.Forms.Button();
            this.tb_input = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_output = new System.Windows.Forms.TextBox();
            this.btn_change_putout = new System.Windows.Forms.Button();
            this.btn_formmat = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lb_info = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_change_input
            // 
            this.btn_change_input.Location = new System.Drawing.Point(482, 36);
            this.btn_change_input.Name = "btn_change_input";
            this.btn_change_input.Size = new System.Drawing.Size(85, 26);
            this.btn_change_input.TabIndex = 0;
            this.btn_change_input.Text = "更换";
            this.btn_change_input.UseVisualStyleBackColor = true;
            // 
            // tb_input
            // 
            this.tb_input.Location = new System.Drawing.Point(90, 39);
            this.tb_input.Name = "tb_input";
            this.tb_input.ReadOnly = true;
            this.tb_input.Size = new System.Drawing.Size(376, 21);
            this.tb_input.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "输入文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "输出文件";
            // 
            // tb_output
            // 
            this.tb_output.Location = new System.Drawing.Point(90, 82);
            this.tb_output.Name = "tb_output";
            this.tb_output.ReadOnly = true;
            this.tb_output.Size = new System.Drawing.Size(376, 21);
            this.tb_output.TabIndex = 4;
            // 
            // btn_change_putout
            // 
            this.btn_change_putout.Location = new System.Drawing.Point(482, 79);
            this.btn_change_putout.Name = "btn_change_putout";
            this.btn_change_putout.Size = new System.Drawing.Size(85, 26);
            this.btn_change_putout.TabIndex = 3;
            this.btn_change_putout.Text = "更换";
            this.btn_change_putout.UseVisualStyleBackColor = true;
            // 
            // btn_formmat
            // 
            this.btn_formmat.Location = new System.Drawing.Point(486, 136);
            this.btn_formmat.Name = "btn_formmat";
            this.btn_formmat.Size = new System.Drawing.Size(75, 23);
            this.btn_formmat.TabIndex = 6;
            this.btn_formmat.Text = "生成";
            this.btn_formmat.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lb_info});
            this.statusStrip1.Location = new System.Drawing.Point(0, 187);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(612, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lb_info
            // 
            this.lb_info.Name = "lb_info";
            this.lb_info.Size = new System.Drawing.Size(47, 17);
            this.lb_info.Text = "就绪.....";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 209);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_formmat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_output);
            this.Controls.Add(this.btn_change_putout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_input);
            this.Controls.Add(this.btn_change_input);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生成工具";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_change_input;
        private System.Windows.Forms.TextBox tb_input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_output;
        private System.Windows.Forms.Button btn_change_putout;
        private System.Windows.Forms.Button btn_formmat;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lb_info;
    }
}

