@tool
extends EditorPlugin

const AUTOLOAD_NAME = "LongSceneManager"

func _enable_plugin() -> void:
	add_autoload_singleton(
		AUTOLOAD_NAME,
        "res://addons/long_scene_manager/autoload/LongSceneManagerCs.cs"
	)

func _disable_plugin() -> void:
	remove_autoload_singleton(AUTOLOAD_NAME)



func _exit_tree() -> void:
	remove_custom_type("LongSceneManager")
