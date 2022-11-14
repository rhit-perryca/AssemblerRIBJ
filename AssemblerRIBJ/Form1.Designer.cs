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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.input = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buildBtn = new System.Windows.Forms.Button();
            this.showOriginal = new System.Windows.Forms.CheckBox();
            this.seps = new System.Windows.Forms.CheckBox();
            this.output = new System.Windows.Forms.RichTextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.input);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.output);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1038, 450);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // input
            // 
            this.input.BackColor = System.Drawing.Color.DimGray;
            this.input.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.input.Font = new System.Drawing.Font("Cascadia Code", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input.ForeColor = System.Drawing.Color.Lime;
            this.input.Location = new System.Drawing.Point(3, 3);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(378, 447);
            this.input.TabIndex = 0;
            this.input.Text = "";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.buildBtn);
            this.flowLayoutPanel2.Controls.Add(this.showOriginal);
            this.flowLayoutPanel2.Controls.Add(this.seps);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(387, 189);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(140, 75);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // buildBtn
            // 
            this.buildBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buildBtn.AutoSize = true;
            this.buildBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buildBtn.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buildBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buildBtn.Location = new System.Drawing.Point(12, 3);
            this.buildBtn.Name = "buildBtn";
            this.buildBtn.Size = new System.Drawing.Size(116, 23);
            this.buildBtn.TabIndex = 1;
            this.buildBtn.Text = "Assemble";
            this.buildBtn.UseVisualStyleBackColor = true;
            this.buildBtn.Click += new System.EventHandler(this.makeCode);
            // 
            // showOriginal
            // 
            this.showOriginal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.showOriginal.AutoSize = true;
            this.showOriginal.BackColor = System.Drawing.Color.Transparent;
            this.showOriginal.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.showOriginal.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showOriginal.ForeColor = System.Drawing.Color.Lime;
            this.showOriginal.Location = new System.Drawing.Point(3, 32);
            this.showOriginal.Name = "showOriginal";
            this.showOriginal.Size = new System.Drawing.Size(134, 17);
            this.showOriginal.TabIndex = 2;
            this.showOriginal.Text = "Show original code";
            this.showOriginal.UseVisualStyleBackColor = false;
            this.showOriginal.CheckedChanged += new System.EventHandler(this.showOriginal_CheckedChanged);
            // 
            // seps
            // 
            this.seps.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.seps.AutoSize = true;
            this.seps.BackColor = System.Drawing.Color.Transparent;
            this.seps.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.seps.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seps.ForeColor = System.Drawing.Color.Lime;
            this.seps.Location = new System.Drawing.Point(12, 55);
            this.seps.Name = "seps";
            this.seps.Size = new System.Drawing.Size(116, 17);
            this.seps.TabIndex = 3;
            this.seps.Text = "Show seperators";
            this.seps.UseVisualStyleBackColor = false;
            this.seps.CheckedChanged += new System.EventHandler(this.seps_CheckedChanged);
            // 
            // output
            // 
            this.output.BackColor = System.Drawing.Color.DimGray;
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output.Font = new System.Drawing.Font("Cascadia Code", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output.ForeColor = System.Drawing.Color.Lime;
            this.output.Location = new System.Drawing.Point(533, 3);
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(497, 447);
            this.output.TabIndex = 2;
            this.output.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1038, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "RIBJ Assembler";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RichTextBox input;
        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buildBtn;
        private System.Windows.Forms.CheckBox showOriginal;
        private System.Windows.Forms.CheckBox seps;
    }
}

