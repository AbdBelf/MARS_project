xof 0303txt 0032
template Vector {
 <3d82ab5e-62da-11cf-ab39-0020af71e433>
 FLOAT x;
 FLOAT y;
 FLOAT z;
}

template MeshFace {
 <3d82ab5f-62da-11cf-ab39-0020af71e433>
 DWORD nFaceVertexIndices;
 array DWORD faceVertexIndices[nFaceVertexIndices];
}

template Mesh {
 <3d82ab44-62da-11cf-ab39-0020af71e433>
 DWORD nVertices;
 array Vector vertices[nVertices];
 DWORD nFaces;
 array MeshFace faces[nFaces];
 [...]
}

template MeshNormals {
 <f6f23f43-7686-11cf-8f52-0040333594a3>
 DWORD nNormals;
 array Vector normals[nNormals];
 DWORD nFaceNormals;
 array MeshFace faceNormals[nFaceNormals];
}

template Coords2d {
 <f6f23f44-7686-11cf-8f52-0040333594a3>
 FLOAT u;
 FLOAT v;
}

template MeshTextureCoords {
 <f6f23f40-7686-11cf-8f52-0040333594a3>
 DWORD nTextureCoords;
 array Coords2d textureCoords[nTextureCoords];
}

template ColorRGBA {
 <35ff44e0-6c7c-11cf-8f52-0040333594a3>
 FLOAT red;
 FLOAT green;
 FLOAT blue;
 FLOAT alpha;
}

template ColorRGB {
 <d3e16e81-7835-11cf-8f52-0040333594a3>
 FLOAT red;
 FLOAT green;
 FLOAT blue;
}

template Material {
 <3d82ab4d-62da-11cf-ab39-0020af71e433>
 ColorRGBA faceColor;
 FLOAT power;
 ColorRGB specularColor;
 ColorRGB emissiveColor;
 [...]
}

template MeshMaterialList {
 <f6f23f42-7686-11cf-8f52-0040333594a3>
 DWORD nMaterials;
 DWORD nFaceIndexes;
 array DWORD faceIndexes[nFaceIndexes];
 [Material <3d82ab4d-62da-11cf-ab39-0020af71e433>]
}

template TextureFilename {
 <a42790e1-7810-11cf-8f52-0040333594a3>
 STRING filename;
}

template VertexDuplicationIndices {
 <b8d65549-d7c9-4995-89cf-53a9a8b031e3>
 DWORD nIndices;
 DWORD nOriginalVertices;
 array DWORD indices[nIndices];
}


Mesh {
 124;
 -5.000000;-2.000000;0.000000;,
 -5.000000;0.000000;5.000000;,
 -5.000000;-2.000000;5.000000;,
 -5.000000;0.000000;0.000000;,
 -5.000000;-2.000000;-5.000000;,
 -5.000000;0.000000;0.000000;,
 -5.000000;-2.000000;0.000000;,
 -5.000000;0.000000;-5.000000;,
 5.000000;-2.000000;0.000000;,
 5.000000;0.000000;-5.000000;,
 5.000000;-2.000000;-5.000000;,
 5.000000;0.000000;0.000000;,
 5.000000;-2.000000;5.000000;,
 5.000000;0.000000;0.000000;,
 5.000000;-2.000000;0.000000;,
 5.000000;0.000000;5.000000;,
 0.000000;-2.000000;-5.000000;,
 -5.000000;0.000000;-5.000000;,
 -5.000000;-2.000000;-5.000000;,
 0.000000;0.000000;-5.000000;,
 5.000000;-2.000000;-5.000000;,
 0.000000;0.000000;-5.000000;,
 0.000000;-2.000000;-5.000000;,
 5.000000;0.000000;-5.000000;,
 0.000000;-2.000000;5.000000;,
 5.000000;0.000000;5.000000;,
 5.000000;-2.000000;5.000000;,
 0.000000;0.000000;5.000000;,
 -5.000000;-2.000000;5.000000;,
 0.000000;0.000000;5.000000;,
 0.000000;-2.000000;5.000000;,
 -5.000000;0.000000;5.000000;,
 10.000000;0.000000;0.000000;,
 5.000000;0.000000;5.000000;,
 10.000000;0.000000;5.000000;,
 5.000000;0.000000;0.000000;,
 10.000000;0.000000;-5.000000;,
 5.000000;0.000000;0.000000;,
 10.000000;0.000000;0.000000;,
 5.000000;0.000000;-5.000000;,
 -5.000000;0.000000;0.000000;,
 -10.000000;0.000000;5.000000;,
 -5.000000;0.000000;5.000000;,
 -10.000000;0.000000;0.000000;,
 -5.000000;0.000000;-5.000000;,
 -10.000000;0.000000;0.000000;,
 -5.000000;0.000000;0.000000;,
 -10.000000;0.000000;-5.000000;,
 10.000000;0.000000;5.000000;,
 5.000000;0.000000;10.000000;,
 10.000000;0.000000;10.000000;,
 5.000000;0.000000;5.000000;,
 10.000000;0.000000;-10.000000;,
 5.000000;0.000000;-5.000000;,
 10.000000;0.000000;-5.000000;,
 5.000000;0.000000;-10.000000;,
 5.000000;0.000000;5.000000;,
 0.000000;0.000000;10.000000;,
 5.000000;0.000000;10.000000;,
 0.000000;0.000000;5.000000;,
 5.000000;0.000000;-10.000000;,
 0.000000;0.000000;-5.000000;,
 5.000000;0.000000;-5.000000;,
 0.000000;0.000000;-10.000000;,
 0.000000;0.000000;5.000000;,
 -5.000000;0.000000;10.000000;,
 0.000000;0.000000;10.000000;,
 -5.000000;0.000000;5.000000;,
 0.000000;0.000000;-10.000000;,
 -5.000000;0.000000;-5.000000;,
 0.000000;0.000000;-5.000000;,
 -5.000000;0.000000;-10.000000;,
 -5.000000;0.000000;5.000000;,
 -10.000000;0.000000;10.000000;,
 -5.000000;0.000000;10.000000;,
 -10.000000;0.000000;5.000000;,
 -5.000000;0.000000;-10.000000;,
 -10.000000;0.000000;-5.000000;,
 -5.000000;0.000000;-5.000000;,
 -10.000000;0.000000;-10.000000;,
 -5.000000;-2.000000;-5.000000;,
 5.000000;-2.000000;5.000000;,
 5.000000;-2.000000;-5.000000;,
 -5.000000;-2.000000;5.000000;,
 0.000000;-2.000000;-2.000000;,
 -2.000000;3.000000;0.000000;,
 0.000000;3.000000;-2.000000;,
 -2.000000;-2.000000;0.000000;,
 2.000000;-2.000000;0.000000;,
 0.000000;3.000000;-2.000000;,
 2.000000;3.000000;0.000000;,
 0.000000;-2.000000;-2.000000;,
 0.000000;-2.000000;2.000000;,
 2.000000;3.000000;0.000000;,
 0.000000;3.000000;2.000000;,
 2.000000;-2.000000;0.000000;,
 -2.000000;-2.000000;0.000000;,
 0.000000;3.000000;2.000000;,
 -2.000000;3.000000;0.000000;,
 0.000000;-2.000000;2.000000;,
 7.000000;0.000000;5.000000;,
 8.000000;3.000000;6.000000;,
 8.000000;0.000000;6.000000;,
 7.000000;3.000000;5.000000;,
 8.000000;0.000000;6.000000;,
 6.000000;3.000000;8.000000;,
 6.000000;0.000000;8.000000;,
 8.000000;3.000000;6.000000;,
 6.000000;0.000000;8.000000;,
 5.000000;3.000000;7.000000;,
 5.000000;0.000000;7.000000;,
 6.000000;3.000000;8.000000;,
 5.000000;0.000000;7.000000;,
 7.000000;3.000000;5.000000;,
 7.000000;0.000000;5.000000;,
 5.000000;3.000000;7.000000;,
 -2.000000;3.000000;0.000000;,
 2.000000;3.000000;0.000000;,
 0.000000;3.000000;-2.000000;,
 0.000000;3.000000;2.000000;,
 5.000000;3.000000;7.000000;,
 8.000000;3.000000;6.000000;,
 7.000000;3.000000;5.000000;,
 6.000000;3.000000;8.000000;;
 62;
 3;0,1,2;,
 3;0,3,1;,
 3;4,5,6;,
 3;4,7,5;,
 3;8,9,10;,
 3;8,11,9;,
 3;12,13,14;,
 3;12,15,13;,
 3;16,17,18;,
 3;16,19,17;,
 3;20,21,22;,
 3;20,23,21;,
 3;24,25,26;,
 3;24,27,25;,
 3;28,29,30;,
 3;28,31,29;,
 3;32,33,34;,
 3;35,33,32;,
 3;36,37,38;,
 3;39,37,36;,
 3;40,41,42;,
 3;43,41,40;,
 3;44,45,46;,
 3;47,45,44;,
 3;48,49,50;,
 3;51,49,48;,
 3;52,53,54;,
 3;55,53,52;,
 3;56,57,58;,
 3;59,57,56;,
 3;60,61,62;,
 3;63,61,60;,
 3;64,65,66;,
 3;67,65,64;,
 3;68,69,70;,
 3;71,69,68;,
 3;72,73,74;,
 3;75,73,72;,
 3;76,77,78;,
 3;79,77,76;,
 3;80,81,82;,
 3;80,83,81;,
 3;84,85,86;,
 3;84,87,85;,
 3;88,89,90;,
 3;88,91,89;,
 3;92,93,94;,
 3;92,95,93;,
 3;96,97,98;,
 3;96,99,97;,
 3;100,101,102;,
 3;100,103,101;,
 3;104,105,106;,
 3;104,107,105;,
 3;108,109,110;,
 3;108,111,109;,
 3;112,113,114;,
 3;112,115,113;,
 3;116,117,118;,
 3;116,119,117;,
 3;120,121,122;,
 3;120,123,121;;

 MeshNormals {
  124;
  10.000000;0.000000;0.000000;,
  10.000000;0.000000;0.000000;,
  10.000000;0.000000;0.000000;,
  10.000000;0.000000;0.000000;,
  10.000000;0.000000;0.000000;,
  10.000000;0.000000;0.000000;,
  10.000000;0.000000;0.000000;,
  10.000000;0.000000;0.000000;,
  -10.000000;0.000000;0.000000;,
  -10.000000;0.000000;0.000000;,
  -10.000000;0.000000;0.000000;,
  -10.000000;0.000000;0.000000;,
  -10.000000;0.000000;0.000000;,
  -10.000000;0.000000;0.000000;,
  -10.000000;0.000000;0.000000;,
  -10.000000;0.000000;0.000000;,
  0.000000;0.000000;10.000000;,
  0.000000;0.000000;10.000000;,
  0.000000;0.000000;10.000000;,
  0.000000;0.000000;10.000000;,
  0.000000;0.000000;10.000000;,
  0.000000;0.000000;10.000000;,
  0.000000;0.000000;10.000000;,
  0.000000;0.000000;10.000000;,
  0.000000;0.000000;-10.000000;,
  0.000000;0.000000;-10.000000;,
  0.000000;0.000000;-10.000000;,
  0.000000;0.000000;-10.000000;,
  0.000000;0.000000;-10.000000;,
  0.000000;0.000000;-10.000000;,
  0.000000;0.000000;-10.000000;,
  0.000000;0.000000;-10.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;100.000000;0.000000;,
  0.000000;100.000000;0.000000;,
  0.000000;100.000000;0.000000;,
  0.000000;100.000000;0.000000;,
  -10.000000;0.000000;-10.000000;,
  -10.000000;0.000000;-10.000000;,
  -10.000000;0.000000;-10.000000;,
  -10.000000;0.000000;-10.000000;,
  10.000000;0.000000;-10.000000;,
  10.000000;0.000000;-10.000000;,
  10.000000;0.000000;-10.000000;,
  10.000000;0.000000;-10.000000;,
  10.000000;0.000000;10.000000;,
  10.000000;0.000000;10.000000;,
  10.000000;0.000000;10.000000;,
  10.000000;0.000000;10.000000;,
  -10.000000;0.000000;10.000000;,
  -10.000000;0.000000;10.000000;,
  -10.000000;0.000000;10.000000;,
  -10.000000;0.000000;10.000000;,
  3.000000;0.000000;-3.000000;,
  3.000000;0.000000;-3.000000;,
  3.000000;0.000000;-3.000000;,
  3.000000;0.000000;-3.000000;,
  6.000000;0.000000;6.000000;,
  6.000000;0.000000;6.000000;,
  6.000000;0.000000;6.000000;,
  6.000000;0.000000;6.000000;,
  -3.000000;0.000000;3.000000;,
  -3.000000;0.000000;3.000000;,
  -3.000000;0.000000;3.000000;,
  -3.000000;0.000000;3.000000;,
  -6.000000;0.000000;-6.000000;,
  -6.000000;0.000000;-6.000000;,
  -6.000000;0.000000;-6.000000;,
  -6.000000;0.000000;-6.000000;,
  0.000000;8.000000;0.000000;,
  0.000000;8.000000;0.000000;,
  0.000000;8.000000;0.000000;,
  0.000000;8.000000;0.000000;,
  0.000000;4.000000;0.000000;,
  0.000000;4.000000;0.000000;,
  0.000000;4.000000;0.000000;,
  0.000000;4.000000;0.000000;;
  62;
  3;0,1,2;,
  3;0,3,1;,
  3;4,5,6;,
  3;4,7,5;,
  3;8,9,10;,
  3;8,11,9;,
  3;12,13,14;,
  3;12,15,13;,
  3;16,17,18;,
  3;16,19,17;,
  3;20,21,22;,
  3;20,23,21;,
  3;24,25,26;,
  3;24,27,25;,
  3;28,29,30;,
  3;28,31,29;,
  3;32,33,34;,
  3;35,33,32;,
  3;36,37,38;,
  3;39,37,36;,
  3;40,41,42;,
  3;43,41,40;,
  3;44,45,46;,
  3;47,45,44;,
  3;48,49,50;,
  3;51,49,48;,
  3;52,53,54;,
  3;55,53,52;,
  3;56,57,58;,
  3;59,57,56;,
  3;60,61,62;,
  3;63,61,60;,
  3;64,65,66;,
  3;67,65,64;,
  3;68,69,70;,
  3;71,69,68;,
  3;72,73,74;,
  3;75,73,72;,
  3;76,77,78;,
  3;79,77,76;,
  3;80,81,82;,
  3;80,83,81;,
  3;84,85,86;,
  3;84,87,85;,
  3;88,89,90;,
  3;88,91,89;,
  3;92,93,94;,
  3;92,95,93;,
  3;96,97,98;,
  3;96,99,97;,
  3;100,101,102;,
  3;100,103,101;,
  3;104,105,106;,
  3;104,107,105;,
  3;108,109,110;,
  3;108,111,109;,
  3;112,113,114;,
  3;112,115,113;,
  3;116,117,118;,
  3;116,119,117;,
  3;120,121,122;,
  3;120,123,121;;
 }

 MeshTextureCoords {
  124;
  0.000000;0.000000;,
  1.000000;0.200000;,
  1.000000;0.000000;,
  0.000000;0.200000;,
  0.000000;0.000000;,
  1.000000;0.200000;,
  1.000000;0.000000;,
  0.000000;0.200000;,
  0.000000;0.000000;,
  1.000000;0.200000;,
  1.000000;0.000000;,
  0.000000;0.200000;,
  0.000000;0.000000;,
  1.000000;0.200000;,
  1.000000;0.000000;,
  0.000000;0.200000;,
  0.000000;0.000000;,
  1.000000;0.200000;,
  1.000000;0.000000;,
  0.000000;0.200000;,
  0.000000;0.000000;,
  1.000000;0.200000;,
  1.000000;0.000000;,
  0.000000;0.200000;,
  0.000000;0.000000;,
  1.000000;0.200000;,
  1.000000;0.000000;,
  0.000000;0.200000;,
  0.000000;0.000000;,
  1.000000;0.200000;,
  1.000000;0.000000;,
  0.000000;0.200000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;0.000000;,
  0.000000;0.000000;,
  1.000000;1.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  0.500000;1.000000;,
  0.500000;0.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  0.500000;1.000000;,
  0.500000;0.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  0.500000;1.000000;,
  0.500000;0.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  0.500000;1.000000;,
  0.500000;0.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  0.500000;1.000000;,
  0.500000;0.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;1.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  0.500000;1.000000;,
  0.500000;0.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;1.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;1.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;1.000000;,
  1.000000;0.000000;,
  0.000000;1.000000;;
 }

 MeshMaterialList {
  4;
  62;
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  1,
  1,
  2,
  2,
  2,
  2,
  2,
  2,
  2,
  2,
  2,
  2,
  2,
  2,
  2,
  2,
  2,
  2,
  3,
  3,
  3,
  3;

  Material {
   1.000000;1.000000;1.000000;1.000000;;
   0.000000;
   0.000000;0.000000;0.000000;;
   0.000000;0.000000;0.000000;;

   TextureFilename {
    "brick.jpg";
   }
  }

  Material {
   1.000000;1.000000;1.000000;1.000000;;
   0.000000;
   0.000000;0.000000;0.000000;;
   0.000000;0.000000;0.000000;;

   TextureFilename {
    "water.jpg";
   }
  }

  Material {
   1.000000;1.000000;1.000000;1.000000;;
   0.000000;
   0.000000;0.000000;0.000000;;
   0.000000;0.000000;0.000000;;

   TextureFilename {
    "wood.jpg";
   }
  }

  Material {
   1.000000;1.000000;1.000000;1.000000;;
   0.000000;
   0.000000;0.000000;0.000000;;
   0.000000;0.000000;0.000000;;

   TextureFilename {
    "woodend.jpg";
   }
  }
 }

 VertexDuplicationIndices {
  124;
  52;
  0,
  1,
  2,
  3,
  4,
  3,
  0,
  7,
  8,
  9,
  10,
  11,
  12,
  11,
  8,
  15,
  16,
  7,
  4,
  19,
  10,
  19,
  16,
  9,
  24,
  15,
  12,
  27,
  2,
  27,
  24,
  1,
  32,
  15,
  34,
  11,
  36,
  11,
  32,
  9,
  3,
  41,
  1,
  43,
  7,
  43,
  3,
  47,
  34,
  49,
  50,
  15,
  52,
  9,
  36,
  55,
  15,
  57,
  49,
  27,
  55,
  19,
  9,
  63,
  27,
  65,
  57,
  1,
  63,
  7,
  19,
  71,
  1,
  73,
  65,
  41,
  71,
  47,
  7,
  79,
  80,
  81,
  82,
  83,
  84,
  85,
  86,
  87,
  88,
  86,
  90,
  84,
  92,
  90,
  94,
  88,
  87,
  94,
  85,
  92,
  100,
  101,
  102,
  103,
  102,
  105,
  106,
  101,
  106,
  109,
  110,
  105,
  110,
  103,
  100,
  109,
  85,
  90,
  86,
  94,
  109,
  101,
  103,
  105;
 }
}