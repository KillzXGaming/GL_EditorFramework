﻿using GL_EditorFramework.GL_Core;
using GL_EditorFramework.Interfaces;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GL_EditorFramework.EditorDrawables.EditableObject;
using static GL_EditorFramework.EditorDrawables.EditorSceneBase;

namespace GL_EditorFramework.EditorDrawables
{
    public abstract class EditableObject : AbstractGlDrawable, IEditableObject
    {

        public static Vector4 hoverColor = new Vector4(1, 1, 1, 1);
        public static Vector4 hoverSelectColor = new Vector4(1, 1, 0.75f, 1);
        public static Vector4 selectColor = new Vector4(1, 1, 0.5f, 1);

        public EditableObject()
        {

        }

        public virtual void StartDragging(DragActionType actionType, int hoveredPart, EditorSceneBase scene)
        {

        }

        public abstract bool IsSelected(int partIndex);

        public abstract bool IsSelectedAll();

        public abstract bool IsSelected();

        public abstract void GetSelectionBox(ref BoundingBox boundingBox);

        public abstract bool IsInRange(float range, float rangeSquared, Vector3 pos);

        public abstract uint SelectAll(GL_ControlBase control);

        public abstract uint SelectDefault(GL_ControlBase control);
        
        public virtual void Draw(GL_ControlModern control, Pass pass, EditorSceneBase editorScene)
        {
            
        }

        public virtual void Draw(GL_ControlLegacy control, Pass pass, EditorSceneBase editorScene)
        {

        }

        public abstract uint Select(int partIndex, GL_ControlBase control);

        public abstract uint Deselect(int partIndex, GL_ControlBase control);
        public abstract uint DeselectAll(GL_ControlBase control);

        public virtual void SetTransform(Vector3? pos, Vector3? rot, Vector3? scale, int part, out Vector3? prevPos, out Vector3? prevRot, out Vector3? prevScale)
        {
            prevPos = null;
            prevRot = null;
            prevScale = null;
        }

        public virtual void ApplyTransformActionToSelection(AbstractTransformAction transformAction, ref TransformChangeInfos transformChangeInfos)
        {
            
        }

        public virtual void ApplyTransformActionToPart(AbstractTransformAction transformAction, int part, ref TransformChangeInfos transformChangeInfos)
        {

        }

        public virtual void DeleteSelected(EditorSceneBase scene, DeletionManager manager, IList list)
        {

        }

        public virtual bool TrySetupObjectUIControl(EditorSceneBase scene, ObjectUIControl objectUIControl) => false;

        public struct BoundingBox
        {
            public float minX;
            public float maxX;
            public float minY;
            public float maxY;
            public float minZ;
            public float maxZ;

            public static BoundingBox Default = new BoundingBox(float.MaxValue, float.MinValue, float.MaxValue, float.MinValue, float.MaxValue, float.MinValue);

            public BoundingBox(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
            {				   
                this.minX = minX;
                this.maxX = maxX;
                this.minY = minY;
                this.maxY = maxY;
                this.minZ = minZ;
                this.maxZ = maxZ;
            }

            public void Include(Vector3 vec)
            {
                if (vec.X < minX)
                    minX = vec.X;
                if (vec.X > maxX)
                    maxX = vec.X;

                if (vec.Y < minY)
                    minY = vec.Y;
                if (vec.Y > maxY)
                    maxY = vec.Y;

                if (vec.Z < minZ)
                    minZ = vec.Z;
                if (vec.Z > maxZ)
                    maxZ = vec.Z;
            }

            public void Include(BoundingBox other)
            {
                if (other.minX < minX)
                    minX = other.minX;
                if (other.maxX > maxX)
                    maxX = other.maxX;

                if (other.minY < minY)
                    minY = other.minY;
                if (other.maxY > maxY)
                    maxY = other.maxY;

                if (other.minZ < minZ)
                    minZ = other.minZ;
                if (other.maxZ > maxZ)
                    maxZ = other.maxZ;
            }

            public Vector3 GetCenter() => new Vector3(
                (minX + maxX) * 0.5f,
                (minY + maxY) * 0.5f,
                (minZ + maxZ) * 0.5f);

            public static bool operator ==(BoundingBox box1, BoundingBox box2)
            {
                return box1.minX == box2.minX &&
                       box1.maxX == box2.maxX &&
                       box1.minY == box2.minY &&
                       box1.maxY == box2.maxY &&
                       box1.minZ == box2.minZ &&
                       box1.maxZ == box2.maxZ;
            }

            public static bool operator !=(BoundingBox box1, BoundingBox box2)
            {
                return box1.minX != box2.minX ||
                       box1.maxX != box2.maxX ||
                       box1.minY != box2.minY ||
                       box1.maxY != box2.maxY ||
                       box1.minZ != box2.minZ ||
                       box1.maxZ != box2.maxZ;
            }
        }

        public struct LocalOrientation
        {
            public Vector3 Origin;
            public Matrix3 Rotation;
            public LocalOrientation(Vector3 origin, Matrix3 rotation)
            {
                Origin = origin;
                Rotation = rotation;
            }

            public LocalOrientation(Vector3 origin)
            {
                Origin = origin;
                Rotation = Matrix3.Identity;
            }
        }

        public virtual void ListChanged(IList list)
        {

        }

        public abstract Vector3 GetFocusPoint();

        public override int GetPickableSpan()
        {
            if (!ObjectRenderState.ShouldBeDrawn(this))
                return 0;
            else
                return 1;
        }
    }

    public interface IEditableObject : ISelectable
    {
        bool Visible { get; set; }

        void StartDragging(DragActionType actionType, int hoveredPart, EditorSceneBase scene);

        bool IsSelected(int partIndex);

        void GetSelectionBox(ref BoundingBox boundingBox);

        bool IsInRange(float range, float rangeSquared, Vector3 pos);

        void Draw(GL_ControlModern control, Pass pass, EditorSceneBase editorScene);

        void Draw(GL_ControlLegacy control, Pass pass, EditorSceneBase editorScene);

        uint Select(int partIndex, GL_ControlBase control);

        uint Deselect(int partIndex, GL_ControlBase control);

        void SetTransform(Vector3? pos, Vector3? rot, Vector3? scale, int part, out Vector3? prevPos, out Vector3? prevRot, out Vector3? prevScale);

        void ApplyTransformActionToSelection(AbstractTransformAction transformAction, ref TransformChangeInfos transformChangeInfos);

        void ApplyTransformActionToPart(AbstractTransformAction transformAction, int part, ref TransformChangeInfos transformChangeInfos);

        void Prepare(GL_ControlModern control);

        void Prepare(GL_ControlLegacy control);

        void Draw(GL_ControlModern control, Pass pass);

        void Draw(GL_ControlLegacy control, Pass pass);

        int GetPickableSpan();

        uint MouseEnter(int index, GL_ControlBase control);

        uint MouseLeave(int index, GL_ControlBase control);

        uint MouseLeaveEntirely(GL_ControlBase control);

        uint MouseDown(MouseEventArgs e, GL_ControlBase control);

        uint MouseMove(MouseEventArgs e, Point lastMousePos, GL_ControlBase control);

        uint MouseUp(MouseEventArgs e, GL_ControlBase control);

        uint MouseWheel(MouseEventArgs e, GL_ControlBase control);

        uint MouseClick(MouseEventArgs e, GL_ControlBase control);

        uint KeyDown(KeyEventArgs e, GL_ControlBase control);

        uint KeyUp(KeyEventArgs e, GL_ControlBase control);

        void DeleteSelected(EditorSceneBase scene, DeletionManager manager, IList list);

        bool TrySetupObjectUIControl(EditorSceneBase scene, ObjectUIControl objectUIControl);

        void ListChanged(IList list);

        Vector3 GetFocusPoint();
    }

    public interface ISelectable
    {
        uint SelectAll(GL_ControlBase control);

        uint SelectDefault(GL_ControlBase control);

        uint DeselectAll(GL_ControlBase control);

        bool IsSelectedAll();

        bool IsSelected();
    }
}
