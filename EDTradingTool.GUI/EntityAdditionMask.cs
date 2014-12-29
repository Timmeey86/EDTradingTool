﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDTradingTool.GUI
{
    /// <summary>
    /// This is a generic class for input masks which allow the addition (i.e. creation) of a data set.
    /// </summary>
    /// <typeparam name="T">The type of the entities to create.</typeparam>
    public class EntityAdditionMask<T> : UserControl, IRequiresEntityHandler where T : Entity.EntityWithIdAndName, new()
    {
        private TableLayoutPanel _tableLayoutPanel = new TableLayoutPanel();
        private TextBox _entityTextBox = new TextBox();

        /// <summary>
        /// The list of parent types. This is stored separately to presever the order.
        /// </summary>
        private List<Type> _parentTypes;
        /// <summary>
        /// A dictionary which contains the combo box for each parent type.
        /// </summary>
        private Dictionary<Type, ComboBox> _parentComboBoxDict = new Dictionary<Type, ComboBox>();
        private Core.IEntityHandler _entityHandler;

        private int _numberOfRows = 0;

        public EntityAdditionMask(String readableEntityName, List<Type> parentTypes = null, List<String> parentReadableTypeNames = null)
            : base()
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;
            this._tableLayoutPanel.Dock = DockStyle.Fill;
            this.Controls.Add(_tableLayoutPanel);

            InitTextBox(readableEntityName);
            InitComboBoxes(parentTypes, parentReadableTypeNames);
            InitAddButton(readableEntityName);
        }

        public void Initialize(Core.IEntityHandler entityHandler)
        {
            _entityHandler = entityHandler;
        }

        protected void InitTextBox(String readableEntityName)
        {
            var label = new Label()
            {
                Name = "EntityNameLabel",
                Dock = DockStyle.Fill,
                Text = readableEntityName,
                TextAlign = ContentAlignment.MiddleLeft,
            };
            _entityTextBox.Dock = DockStyle.Fill;

            _tableLayoutPanel.Controls.Add(label, 0, _numberOfRows);
            _tableLayoutPanel.Controls.Add(_entityTextBox, 1, _numberOfRows);
            _numberOfRows++;
        }

        protected void InitComboBoxes(List<Type> parentTypes, List<String> parentReadableTypeNames)
        {
            if (parentTypes == null || parentTypes.Count == 0) return;
            if (parentReadableTypeNames == null || parentReadableTypeNames.Count == 0) return;

            _parentTypes = parentTypes;
            for (int index = 0; index < parentTypes.Count; index++)
            {
                var parentType = parentTypes[index];
                var readableName = parentReadableTypeNames[index];

                var label = new Label()
                {
                    Name = parentType.ToString() + "Label",
                    Dock = DockStyle.Fill,
                    Text = readableName,
                    TextAlign = ContentAlignment.MiddleLeft
                };
                _tableLayoutPanel.Controls.Add(label, 0, _numberOfRows);

                var entityComboBoxType = typeof(EntityComboBox<>);
                var comboBox = (ComboBox)Activator.CreateInstance(entityComboBoxType.MakeGenericType(new Type[] { parentType }));
                comboBox.Name = parentType.ToString() + "ComboBox";
                comboBox.Dock = DockStyle.Fill;

                _parentComboBoxDict.Add(parentType, comboBox);
                _tableLayoutPanel.Controls.Add(comboBox, 1, _numberOfRows);

                _numberOfRows++;
            }
        }

        private void InitAddButton(String readableEntityName)
        {
            var addButton = new Button()
            {
                Name = "AddButton",
                Text = "Add " + readableEntityName,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true
            };

            _tableLayoutPanel.Controls.Add(addButton, 1, _numberOfRows);
            _numberOfRows++;

            addButton.Click += OnAddButtonClicked;
        }

        void OnAddButtonClicked(object sender, EventArgs e)
        {
            if (_entityTextBox.Text.Length == 0) return;
            foreach (var parentComboBox in _parentComboBoxDict.Values)
            {
                if (parentComboBox.SelectedItem == null) return;
            }

            var dataSet = new T()
            {
                Name = _entityTextBox.Text
            };
            var parentDataSets = new List<Core.IEntity>();
            if (_parentTypes != null)
            {
                foreach (var parentType in _parentTypes)
                {
                    parentDataSets.Add((Core.IEntity)_parentComboBoxDict[parentType].SelectedItem);
                }
            }

            try
            {
                _entityHandler.AddObject(dataSet, parentDataSets.ToArray());
                MessageBox.Show(
                    String.Format("Successfully added {0} \"{1}\".", _tableLayoutPanel.Controls["EntityNameLabel"].Text, dataSet.Name),
                    Application.ProductName
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // EntityAdditionMask
            // 
            this.Name = "EntityAdditionMask";
            this.Size = new System.Drawing.Size(151, 26);
            this.ResumeLayout(false);

        }
    }
}