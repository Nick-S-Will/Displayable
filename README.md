This Unity package is meant to facilitate creating and handling collections of displays for any kind of object.

To get started:
- Derive one of the display classes with the object type you want to display and override the UpdateGraphics method to choose how it is displayed
- Derive the DisplayMaker class with that new display type and the object type and use its inherited methods to decide which objects to display and when
