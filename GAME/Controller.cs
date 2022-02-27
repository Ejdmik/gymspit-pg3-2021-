using System;


namespace Lecture19Composition
{
	interface IController
	{
		string ChooseAction(Character character, Character enemy);
	}
}
