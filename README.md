# Displayable

### Summary

Unity package to facilitate making and handling displays for any class.



### Getting Started

1. Make a subclass of **Display<ObjectType>** (or one of its children) with the *object type* you want to display and override the **UpdateVisuals** method to choose how the *object* is displayed.
2. Make a subclass of **DisplayMaker<ObjectType, DisplayType>** with the same *object type* and your display subclass.
3. Make a prefab with your display subclass on it.
4. Add your display maker subclass to a game object and assign a parent and your prefab. It can now be given *objects* to display.



### Optional

* **Display<ObjectType>**'s **SetVisible** method can be overridden to specify how it gets shown/hidden.
* **DisplayMaker<ObjectType, DisplayType>**'s **DisplayComparison** can be overridden to sort displays.

