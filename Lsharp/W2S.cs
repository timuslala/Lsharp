using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Media;
using SharpDX;
using ExSharpBase.Overlay;

namespace Lsharp
{
	public partial class Program
	{
		public static bool WorldToScreen(System.Numerics.Vector3 pos, out System.Numerics.Vector2 onscreen)
		{

			System.Numerics.Vector2 screen = new System.Numerics.Vector2(DXD.window_size.Right - DXD.window_size.Left, DXD.window_size.Bottom - DXD.window_size.Top);

			Matrix4x4 matrix = getViewProjectionMatrix();
			System.Numerics.Vector4 clipCoords;
			clipCoords.X = pos.X * matrix.M11 + pos.Y * matrix.M21 + pos.Z * matrix.M31 + matrix.M41;
			clipCoords.Y = pos.X * matrix.M12 + pos.Y * matrix.M22 + pos.Z * matrix.M32 + matrix.M42;
			clipCoords.Z = pos.X * matrix.M13 + pos.Y * matrix.M23 + pos.Z * matrix.M33 + matrix.M43;
			clipCoords.W = pos.X * matrix.M14 + pos.Y * matrix.M24 + pos.Z * matrix.M34 + matrix.M44;

			if (clipCoords.W < 0.1f)
			{
				onscreen = new System.Numerics.Vector2(0, 0);
				return false;
			}
			System.Numerics.Vector3 M;
			M.X = clipCoords.X / clipCoords.W;
			M.Y = clipCoords.Y / clipCoords.W;
			M.Z = clipCoords.Z / clipCoords.W;

			onscreen.X = (screen.X / 2 * M.X) + (M.X + screen.X / 2);
			onscreen.Y = -(screen.Y / 2 * M.Y) + (M.Y + screen.Y / 2);
			
			return true;
		}
		static Matrix4x4 getViewProjectionMatrix()
		{
			return (Matrix4x4.Multiply(getViewMatrix(), getProjectionMatrix()));
		}
		static Matrix4x4 getViewMatrix()
		{
			int bytesRead = 0;
			var buffer = new byte[64];
			ReadProcessMemory(processHandle, Renderer_ptr + RENDERER_VIEWMATRIX, buffer, buffer.Length, ref bytesRead);
			return (new Matrix4x4(BitConverter.ToSingle(buffer, 0),
				BitConverter.ToSingle(buffer, 4),
				BitConverter.ToSingle(buffer, 8),
				BitConverter.ToSingle(buffer, 12),
				BitConverter.ToSingle(buffer, 16),
				BitConverter.ToSingle(buffer, 20),
				BitConverter.ToSingle(buffer, 24),
				BitConverter.ToSingle(buffer, 28),
				BitConverter.ToSingle(buffer, 32),
				BitConverter.ToSingle(buffer, 36),
				BitConverter.ToSingle(buffer, 40),
				BitConverter.ToSingle(buffer, 44),
				BitConverter.ToSingle(buffer, 48),
				BitConverter.ToSingle(buffer, 52),
				BitConverter.ToSingle(buffer, 56),
				BitConverter.ToSingle(buffer, 60)
				));

		}
		public static SharpDX.Matrix getSharpViewMatrix()
		{
			int bytesRead = 0;
			var buffer = new byte[64];
			ReadProcessMemory(processHandle, Renderer_ptr + RENDERER_VIEWMATRIX, buffer, buffer.Length, ref bytesRead);
			return (new SharpDX.Matrix(BitConverter.ToSingle(buffer, 0),
				BitConverter.ToSingle(buffer, 4),
				BitConverter.ToSingle(buffer, 8),
				BitConverter.ToSingle(buffer, 12),
				BitConverter.ToSingle(buffer, 16),
				BitConverter.ToSingle(buffer, 20),
				BitConverter.ToSingle(buffer, 24),
				BitConverter.ToSingle(buffer, 28),
				BitConverter.ToSingle(buffer, 32),
				BitConverter.ToSingle(buffer, 36),
				BitConverter.ToSingle(buffer, 40),
				BitConverter.ToSingle(buffer, 44),
				BitConverter.ToSingle(buffer, 48),
				BitConverter.ToSingle(buffer, 52),
				BitConverter.ToSingle(buffer, 56),
				BitConverter.ToSingle(buffer, 60)
				));

		}
		static Matrix4x4 getProjectionMatrix()
		{
			int bytesRead = 0;
			var buffer = new byte[64];
			ReadProcessMemory(processHandle, Renderer_ptr + RENDERER_PROJECTIONMATRIX, buffer, buffer.Length, ref bytesRead);
			return (new Matrix4x4(BitConverter.ToSingle(buffer, 0),
				BitConverter.ToSingle(buffer, 4),
				BitConverter.ToSingle(buffer, 8),
				BitConverter.ToSingle(buffer, 12),
				BitConverter.ToSingle(buffer, 16),
				BitConverter.ToSingle(buffer, 20),
				BitConverter.ToSingle(buffer, 24),
				BitConverter.ToSingle(buffer, 28),
				BitConverter.ToSingle(buffer, 32),
				BitConverter.ToSingle(buffer, 36),
				BitConverter.ToSingle(buffer, 40),
				BitConverter.ToSingle(buffer, 44),
				BitConverter.ToSingle(buffer, 48),
				BitConverter.ToSingle(buffer, 52),
				BitConverter.ToSingle(buffer, 56),
				BitConverter.ToSingle(buffer, 60)
				));
		}
		public static SharpDX.Matrix getSharpProjectionMatrix()
		{
			int bytesRead = 0;
			var buffer = new byte[64];
			ReadProcessMemory(processHandle, Renderer_ptr + RENDERER_PROJECTIONMATRIX, buffer, buffer.Length, ref bytesRead);
			return (new SharpDX.Matrix(BitConverter.ToSingle(buffer, 0),
				BitConverter.ToSingle(buffer, 4),
				BitConverter.ToSingle(buffer, 8),
				BitConverter.ToSingle(buffer, 12),
				BitConverter.ToSingle(buffer, 16),
				BitConverter.ToSingle(buffer, 20),
				BitConverter.ToSingle(buffer, 24),
				BitConverter.ToSingle(buffer, 28),
				BitConverter.ToSingle(buffer, 32),
				BitConverter.ToSingle(buffer, 36),
				BitConverter.ToSingle(buffer, 40),
				BitConverter.ToSingle(buffer, 44),
				BitConverter.ToSingle(buffer, 48),
				BitConverter.ToSingle(buffer, 52),
				BitConverter.ToSingle(buffer, 56),
				BitConverter.ToSingle(buffer, 60)
				));
		}
		/*EXTERNAL HP BARS
		 
		  BYTE v3 = Read<BYTE>(dwObject + 0x4949);
	DWORD v33 = Read<DWORD>(dwObject + 4 * Read<BYTE>(dwObject + 0x4950) + 0x4954);
	if (v3)
	{
		int* v4 = (int*)(dwObject + 0x494C);
		do
		{
			int v5 = Read<int>(v4);
			++v4;
			*(&v33 + (DWORD)v2) ^= ~v5;
			++v2;
		} while ((unsigned int)v2 < v3);
	}
 
	DWORD dwPtr = v33;
		DWORD dwBar2 = dwPtr ? Read<DWORD>(Read<DWORD>(dwPtr + 0x18)+4):0;
		fHPBarOff = Read<float>(Read<DWORD>(dwBar2 + 0x1C) + 0x88);
 
		auto hpPos = pLocal->GetPosition();
		hpPos.y += fHPBarOff;
 
		auto vHp = WorldToScreen(hpPos);
 
 
 
		float fMaxZoom = 2250.f;
		float fZoom = Read<float>(Read<DWORD>(Read<DWORD>(dwBase+ oHudInstance) + 0x0C) + 0x25C);
		float fZoomDelta = fMaxZoom / fZoom;
 
		vHp.x -= 53;
		vHp.y = vHp.y - ((vScreenSize.y * 0.00083333335f * fZoomDelta) * fHPBarOff);
		
		 
*/
		// sig - 33 C9 57 0F B6 7E 01 8B 54 86 0C
		public static int GetNavPtr(int dwObj)
		{
			int v1 = 0; // eax
			int v2 = 0; // esi
			uint v3 = 0; // ecx
			uint v4 = 0; // edi
			int v5 = 0; // edx
			int v6 = 0; // edx
			int v7 = 0; // eax
			byte v8 = 0; // al
			int v9 = 0; // eax
			int v10 = 0; // edx
			byte v11 = 0; // cl
			int v13 = 0; // [esp+8h] [ebp-4h]
			int result = 0;
			v1 = GetObjByte(dwObj+0x3498);
			v2 = (int)(dwObj + 0x3490);
			v3 = 0;
			v4 = GetObjByte(v2 + 1);

			v5 = GetObjInt32(v2 + (4 * v1) + 12);

			v13 = v2 + (4 * v1) + 12;

			if (v4%2==1)
			{
				v6 = (v2 + 4);
				v7 = GetObjInt32(v6);
				++v6;
				int a = GetObjInt32((int)(v13 + v3));
				a ^= ~v7;
				++v3;
				v5 = a;
				return GetObjInt32(a + 8);
			}

			v8 = GetObjByte(v2 + 2);
			
			return v8;
			if (v8%2==0)
			{
				result = 0;
				//return result;
			}
			
			v9 =(4 - v8);

			if (v9 >= 4)
			{
				result = 0;
				return result;
				//return *(_DWORD *)(v5 + 8);
			}


			v10 = ((int)(v9 + v2 + 4));
			do
			{
				v11 = GetObjByte(v10++);
				byte b = GetObjByte((int)(v13 + v9++));
				b ^= (byte)~v11;
			} while (v9 < 4);

			result = GetObjInt32(v13) + 8;
			return result;
		}
	}
}
