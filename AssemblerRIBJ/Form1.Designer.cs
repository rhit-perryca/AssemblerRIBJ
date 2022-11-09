namespace AssemblerRIBJ
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.input = new System.Windows.Forms.RichTextBox();
            this.output = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.copy = new System.Windows.Forms.Button();
            this.showOriginal = new System.Windows.Forms.CheckBox();
            this.assemble = new System.Windows.Forms.Button();
            this.seps = new System.Windows.Forms.CheckBox();
            this.lineNumbers = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 839F));
            this.tableLayoutPanel1.Controls.Add(this.input, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.output, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1440, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // input
            // 
            this.input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.input.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.input.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input.ForeColor = System.Drawing.Color.Lime;
            this.input.Location = new System.Drawing.Point(3, 3);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(376, 444);
            this.input.TabIndex = 0;
            this.input.Text = resources.GetString("input.Text");
            // 
            // output
            // 
            this.output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output.ForeColor = System.Drawing.Color.Lime;
            this.output.Location = new System.Drawing.Point(603, 3);
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(834, 444);
            this.output.TabIndex = 2;
            this.output.Text = "";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lineNumbers, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.copy, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.showOriginal, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.assemble, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.seps, 0, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(385, 90);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(212, 270);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // copy
            // 
            this.copy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.copy.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copy.Location = new System.Drawing.Point(3, 42);
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(206, 33);
            this.copy.TabIndex = 3;
            this.copy.Text = "Copy Machine Code";
            this.copy.UseVisualStyleBackColor = true;
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // showOriginal
            // 
            this.showOriginal.AutoSize = true;
            this.showOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showOriginal.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showOriginal.ForeColor = System.Drawing.Color.Lime;
            this.showOriginal.Location = new System.Drawing.Point(3, 81);
            this.showOriginal.Name = "showOriginal";
            this.showOriginal.Size = new System.Drawing.Size(206, 48);
            this.showOriginal.TabIndex = 1;
            this.showOriginal.Text = "Show original";
            this.showOriginal.UseVisualStyleBackColor = true;
            // 
            // assemble
            // 
            this.assemble.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assemble.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assemble.Location = new System.Drawing.Point(3, 3);
            this.assemble.Name = "assemble";
            this.assemble.Size = new System.Drawing.Size(206, 33);
            this.assemble.TabIndex = 2;
            this.assemble.Text = "Assemble";
            this.assemble.UseVisualStyleBackColor = true;
            this.assemble.Click += new System.EventHandler(this.makeCode);
            // 
            // seps
            // 
            this.seps.AutoSize = true;
            this.seps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seps.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seps.ForeColor = System.Drawing.Color.Lime;
            this.seps.Location = new System.Drawing.Point(3, 135);
            this.seps.Name = "seps";
            this.seps.Size = new System.Drawing.Size(206, 43);
            this.seps.TabIndex = 0;
            this.seps.Text = "Show seperators";
            this.seps.UseVisualStyleBackColor = true;
            // 
            // lineNumbers
            // 
            this.lineNumbers.AutoSize = true;
            this.lineNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineNumbers.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineNumbers.ForeColor = System.Drawing.Color.Lime;
            this.lineNumbers.Location = new System.Drawing.Point(3, 184);
            this.lineNumbers.Name = "lineNumbers";
            this.lineNumbers.Size = new System.Drawing.Size(206, 83);
            this.lineNumbers.TabIndex = 4;
            this.lineNumbers.Text = "Show line numbers";
            this.lineNumbers.UseVisualStyleBackColor = true;
            this.lineNumbers.CheckedChanged += new System.EventHandler(this.lineNumbers_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1440, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "RIBJ Assembler";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox input;
        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox seps;
        private System.Windows.Forms.CheckBox showOriginal;
        private System.Windows.Forms.Button assemble;
        private System.Windows.Forms.Button copy;
        private System.Windows.Forms.CheckBox lineNumbers;
    }
}

