Ashlot SparkEffect1

I've summarized the effects and textures that I added in this version 1.4 to the "Assets/ashlot/effect/spark1_4/new/" folder.


----------------------------------------------------------------------------------------
I summarizes the effect of the previous version to the "Assets/ashlot/effect/spark1_4/before/" folder.

I am at 1.0 and 1.1 versions of the effect to the "Assets/ashlot/effect/spark1_4/before/spark1.1/horizon/" folder.
All effects use the TextureSheetAnimation feature of the ParticleSystem.
By changing the value of the Row of TextureSheetAnimation you can have multiple effects in one TextureSheet.
This will reduce the draw calls of multiple effects.


There are each effect texture sheets of a horizontal row in a folder named pngs.
It has been created with software called TexturePacker,
Pixcel format of TexturePacker was set to RGBA8888 when I create it.

Each of its texture sheets have been created in the (256 × 16) size 256 × 4096.
By placing them vertically, I've created a texture sheet of a new 4096 × 4096 size.
I've also created TextureSheet of 1024 × 1024 size.

Pixcel format of each texturesheets has been written to the file name.
TextureSheet in a folder named ***_PremultiplyAlpha is,
When I create a TextureSheet by TexturePacker, I've set the PremultiplyAlpha.
The color values of TextureSheet, is multiplied by the alpha value.


1024_RGBA4444_PremultiplyAlpha
1024_RGBA4444
1024_RGBA8888_PremultiplyAlpha
1024_RGBA8888
Texture sheet in the folder of the above, I've imported a 1024px × 1024px size.
The default setting for importing has been adapted to the texture,
MaxSize is 1024.


4096_RGBA4444_PremultiplyAlpha
4096_RGBA4444
4096_RGBA8888_PremultiplyAlpha
4096_RGBA8888
Texture sheet in the folder of the above, I've imported a 4096px × 4096px size.
The default setting for importing has been adapted to the texture,
MaxSize is 1024.
Please set a different value if necessary.
(Only in the folder of 4096_RGBA8888, we have created a material that has the MaxSize to 4096.)

Prefabs added what material has been changed.
And, Prefabs added what the Looping and Emission of ParticleSystem has been changed.

If you want to material of size and compression format of another,
Please set the Renderer Materiarl of items ParticleSystem component.
Please set the Material you want to use to Materiarl of Renderer items ParticleSystem component.


-------------------------------------------------------------------------------- 

In folder of "Assets/ashlot/effect/spark1_4/", I created a scene that all_effects_use4096.unity.
This scene, I have put all of the effects until now.

The scene I'm using the UI (uGUI).

Effects of loop settings will disappear when you press the button again.

-------------------------------------------------------------------------------- 


In folder of "Assets/ashlot/effect/spark1_4/before/basic_0/prefab/",
Most of the effects are using the material of "Assets/ashlot/effect/spark1_4/new/texture_material/" folder.
But two effects are using the material of "/ashlot/effect/spark1_4/before/spark1.1/horizon/" folder. 
(basic0_a1_4_1_horizon1row13 , basic0_a4_0_1_horizon1row13) 

basic0_a1_4_0_star0_colorful0 and basic0_a1_4_1_horizon1row13 are the same movement.
basic0_a4_0_0_star0_colorful0 and basic0_a4_0_1_horizon1row13 are the same movement.

I wanted to show that the change is atmosphere by material(the texture).

-------------------------------------------------------------------------------- 

E-mail address of support, etc.
ashlot@ashlot.net
Please send an email.
or
please submit in the forum of SupportSite.
http://ashlot.net/forums/

Demo Scene Web Page
http://ashlot.net/unity_asset/

--------------------------------------------------------------------------------
ReleaseNotes

-ver1.4
2015/08/20
I added new 19 effects.
I added some of the pictures in the texture.  
I plan to add some of the effects in the future. 

-ver1.3
2015/05/27
I added the effects of the new movement to use the same texture as the previous version.
And I added demo scenes. 
I plan to add some of the effects in the future. 

-ver1.2
2015/03/12
I added the basic effects.
crosshatch and the stars and the Heart.


-ver1.1
2014/10/04
Add two effects, the number of effects is now 32.


-ver1.0
2014/09/07
It is the first release.
the number of effects is 30.
