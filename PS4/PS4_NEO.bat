pushd "%~dp0"
orbis-ctrl pkill eboot.bin
echo ^<settings version="1" target="orbis"^> ^<setting key="0x7802B600"  value="0"  /^> ^</settings^> > %tmp%\ps4_settings_allowNEOmode.xml
orbis-ctrl settings-import %tmp%\ps4_settings_allowNEOmode.xml
del %tmp%\ps4_settings_allowNEOmode.xml
orbis-run /fsroot . /console:process /log:"eboot.log" /elf "eboot.bin"
popd
