/* 
 * PROJECT: NyARToolkit(Extension)
 * --------------------------------------------------------------------------------
 * The NyARToolkit is Java edition ARToolKit class library.
 * Copyright (C)2008-2009 Ryo Iizuka
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * For further information please contact.
 *	http://nyatla.jp/nyatoolkit/
 *	<airmail(at)ebony.plala.or.jp> or <nyatla(at)nyatla.jp>
 * 
 */
using jp.nyatla.nyartoolkit.cs.core;
namespace jp.nyatla.nyartoolkit.cs.markersystem.utils
{


    public class SquareStack : NyARObjectStack<SquareStack.Item>
    {
        public class Item : NyARSquare
        {
            public NyARIntPoint2d center2d = new NyARIntPoint2d();
            /** 検出座標系の値*/
            public NyARIntPoint2d[] ob_vertex = NyARIntPoint2d.createArray(4);
            /** 頂点の分布範囲*/
            public NyARIntRect vertex_area = new NyARIntRect();
            /** rectの面積*/
            public int rect_area;
        }
        public SquareStack(int i_length)
        {
            base.initInstance(i_length);
        }
        protected override SquareStack.Item createElement()
        {
            return new SquareStack.Item();
        }
    }
}