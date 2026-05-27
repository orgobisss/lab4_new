namespace lab4_new
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonSaveSelection1 = new System.Windows.Forms.Button();
            this.buttonSaveSelection2 = new System.Windows.Forms.Button();
            this.btnAddHorizontalPin = new System.Windows.Forms.Button();
            this.btnAddVerticalPin = new System.Windows.Forms.Button();
            this.btnAddBase = new System.Windows.Forms.Button();
            this.btnAddClamp = new System.Windows.Forms.Button();
            this.labelStage1 = new System.Windows.Forms.Label();
            this.labelStage2 = new System.Windows.Forms.Label();
            this.labelComponents = new System.Windows.Forms.Label();
            this.panelStage1 = new System.Windows.Forms.Panel();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.panelStage2 = new System.Windows.Forms.Panel();
            this.panelStage1.SuspendLayout();
            this.panelComponents.SuspendLayout();
            this.panelStage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(118)))), ((int)(((byte)(200)))));
            this.buttonOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.buttonOpen.ForeColor = System.Drawing.Color.White;
            this.buttonOpen.Location = new System.Drawing.Point(15, 15);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(200, 40);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "📂 Открыть сборку";
            this.buttonOpen.UseVisualStyleBackColor = false;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // panelStage1
            // 
            this.panelStage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStage1.Controls.Add(this.labelStage1);
            this.panelStage1.Controls.Add(this.buttonSaveSelection1);
            this.panelStage1.Location = new System.Drawing.Point(15, 70);
            this.panelStage1.Name = "panelStage1";
            this.panelStage1.Size = new System.Drawing.Size(200, 100);
            this.panelStage1.TabIndex = 1;
            // 
            // labelStage1
            // 
            this.labelStage1.AutoSize = true;
            this.labelStage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelStage1.Location = new System.Drawing.Point(10, 10);
            this.labelStage1.Name = "labelStage1";
            this.labelStage1.Size = new System.Drawing.Size(178, 17);
            this.labelStage1.TabIndex = 0;
            this.labelStage1.Text = "Этап 1: Выбор базовых граней";
            // 
            // buttonSaveSelection1
            // 
            this.buttonSaveSelection1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.buttonSaveSelection1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSaveSelection1.ForeColor = System.Drawing.Color.White;
            this.buttonSaveSelection1.Location = new System.Drawing.Point(10, 35);
            this.buttonSaveSelection1.Name = "buttonSaveSelection1";
            this.buttonSaveSelection1.Size = new System.Drawing.Size(180, 50);
            this.buttonSaveSelection1.TabIndex = 1;
            this.buttonSaveSelection1.Text = "✓ Сохранить 5 граней\n(этап 1)";
            this.buttonSaveSelection1.UseVisualStyleBackColor = false;
            this.buttonSaveSelection1.Click += new System.EventHandler(this.buttonSaveSelection1_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelComponents.Controls.Add(this.labelComponents);
            this.panelComponents.Controls.Add(this.btnAddHorizontalPin);
            this.panelComponents.Controls.Add(this.btnAddVerticalPin);
            this.panelComponents.Controls.Add(this.btnAddClamp);
            this.panelComponents.Location = new System.Drawing.Point(230, 70);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(200, 180);
            this.panelComponents.TabIndex = 2;
            // 
            // labelComponents
            // 
            this.labelComponents.AutoSize = true;
            this.labelComponents.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelComponents.Location = new System.Drawing.Point(10, 10);
            this.labelComponents.Name = "labelComponents";
            this.labelComponents.Size = new System.Drawing.Size(150, 17);
            this.labelComponents.TabIndex = 3;
            this.labelComponents.Text = "Добавление деталей";
            // 
            // btnAddHorizontalPin
            // 
            this.btnAddHorizontalPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnAddHorizontalPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddHorizontalPin.ForeColor = System.Drawing.Color.White;
            this.btnAddHorizontalPin.Location = new System.Drawing.Point(10, 35);
            this.btnAddHorizontalPin.Name = "btnAddHorizontalPin";
            this.btnAddHorizontalPin.Size = new System.Drawing.Size(180, 35);
            this.btnAddHorizontalPin.TabIndex = 4;
            this.btnAddHorizontalPin.Text = "➕ PalecHorizontal";
            this.btnAddHorizontalPin.UseVisualStyleBackColor = false;
            this.btnAddHorizontalPin.Click += new System.EventHandler(this.btnAddHorizontalPin_Click);
            // 
            // btnAddVerticalPin
            // 
            this.btnAddVerticalPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnAddVerticalPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddVerticalPin.ForeColor = System.Drawing.Color.White;
            this.btnAddVerticalPin.Location = new System.Drawing.Point(10, 75);
            this.btnAddVerticalPin.Name = "btnAddVerticalPin";
            this.btnAddVerticalPin.Size = new System.Drawing.Size(180, 35);
            this.btnAddVerticalPin.TabIndex = 5;
            this.btnAddVerticalPin.Text = "➕ PalecVertical";
            this.btnAddVerticalPin.UseVisualStyleBackColor = false;
            this.btnAddVerticalPin.Click += new System.EventHandler(this.btnAddVerticalPin_Click);
            // 
            // btnAddClamp
            // 
            this.btnAddClamp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnAddClamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddClamp.ForeColor = System.Drawing.Color.White;
            this.btnAddClamp.Location = new System.Drawing.Point(10, 115);
            this.btnAddClamp.Name = "btnAddClamp";
            this.btnAddClamp.Size = new System.Drawing.Size(180, 35);
            this.btnAddClamp.TabIndex = 6;
            this.btnAddClamp.Text = "➕ Clamp";
            this.btnAddClamp.UseVisualStyleBackColor = false;
            this.btnAddClamp.Click += new System.EventHandler(this.btnAddClamp_Click);
            // 
            // panelStage2
            // 
            this.panelStage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStage2.Controls.Add(this.labelStage2);
            this.panelStage2.Controls.Add(this.buttonSaveSelection2);
            this.panelStage2.Controls.Add(this.btnAddBase);
            this.panelStage2.Location = new System.Drawing.Point(445, 70);
            this.panelStage2.Name = "panelStage2";
            this.panelStage2.Size = new System.Drawing.Size(200, 180);
            this.panelStage2.TabIndex = 3;
            // 
            // labelStage2
            // 
            this.labelStage2.AutoSize = true;
            this.labelStage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelStage2.Location = new System.Drawing.Point(10, 10);
            this.labelStage2.Name = "labelStage2";
            this.labelStage2.Size = new System.Drawing.Size(178, 17);
            this.labelStage2.TabIndex = 2;
            this.labelStage2.Text = "Этап 2: Выбор граней паль.";
            // 
            // buttonSaveSelection2
            // 
            this.buttonSaveSelection2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.buttonSaveSelection2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSaveSelection2.ForeColor = System.Drawing.Color.White;
            this.buttonSaveSelection2.Location = new System.Drawing.Point(10, 35);
            this.buttonSaveSelection2.Name = "buttonSaveSelection2";
            this.buttonSaveSelection2.Size = new System.Drawing.Size(180, 50);
            this.buttonSaveSelection2.TabIndex = 7;
            this.buttonSaveSelection2.Text = "✓ Сохранить 3 грани\n(этап 2)";
            this.buttonSaveSelection2.UseVisualStyleBackColor = false;
            this.buttonSaveSelection2.Click += new System.EventHandler(this.buttonSaveSelection2_Click);
            // 
            // btnAddBase
            // 
            this.btnAddBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnAddBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddBase.ForeColor = System.Drawing.Color.White;
            this.btnAddBase.Location = new System.Drawing.Point(10, 90);
            this.btnAddBase.Name = "btnAddBase";
            this.btnAddBase.Size = new System.Drawing.Size(180, 35);
            this.btnAddBase.TabIndex = 8;
            this.btnAddBase.Text = "➕ Base (основание)";
            this.btnAddBase.UseVisualStyleBackColor = false;
            this.btnAddBase.Click += new System.EventHandler(this.btnAddBase_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(660, 270);
            this.Controls.Add(this.panelStage2);
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelStage1);
            this.Controls.Add(this.buttonOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Сопряжение деталей - SOLIDWORKS Assembly Builder";
            this.panelStage1.ResumeLayout(false);
            this.panelStage1.PerformLayout();
            this.panelComponents.ResumeLayout(false);
            this.panelComponents.PerformLayout();
            this.panelStage2.ResumeLayout(false);
            this.panelStage2.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonSaveSelection1;
        private System.Windows.Forms.Button buttonSaveSelection2;
        private System.Windows.Forms.Button btnAddHorizontalPin;
        private System.Windows.Forms.Button btnAddVerticalPin;
        private System.Windows.Forms.Button btnAddBase;
        private System.Windows.Forms.Button btnAddClamp;
        private System.Windows.Forms.Label labelStage1;
        private System.Windows.Forms.Label labelStage2;
        private System.Windows.Forms.Label labelComponents;
        private System.Windows.Forms.Panel panelStage1;
        private System.Windows.Forms.Panel panelComponents;
        private System.Windows.Forms.Panel panelStage2;
    }
}
