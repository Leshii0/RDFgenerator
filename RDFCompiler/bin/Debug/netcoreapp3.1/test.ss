CodeTypeDeclaration mainClass = new CodeTypeDeclaration();
mainClass.Name = "Person";

CodeTypeDeclaration otherClass = new CodeTypeDeclaration();
otherClass.Name = "Car";

CodeTypeDeclaration workerClass = new CodeTypeDeclaration();
workerClass.Name = "Worker";
CodeTypeDeclaration directorClass = new CodeTypeDeclaration();
directorClass.Name = "Director";

workerClass.BaseTypes.Add("Person");
directorClass.BaseTypes.Add("Person");

CodeMemberMethod baseMethod = new CodeMemberMethod();
baseMethod.Name = "Print";
baseMethod.ReturnType = new CodeTypeReference();
baseMethod.Attributes = MemberAttributes.Public;
mainClass.Members.Add(baseMethod);

CodeMemberProperty baseProp = new CodeMemberProperty();
baseProp.Name = "Surname";
baseProp.Type = new CodeTypeReference(typeof(string));
baseProp.Attributes = MemberAttributes.Public | MemberAttributes.Final;
mainClass.Members.Add(baseProp);


CodeMemberProperty workerProp = new CodeMemberProperty();
workerProp.Name = "Company";
workerProp.Type = new CodeTypeReference(typeof(string));
workerProp.Attributes = MemberAttributes.Public | MemberAttributes.Final;
workerClass.Members.Add(workerProp);
directorClass.Members.Add(workerProp);

CodeMemberProperty directorProp = new CodeMemberProperty();
directorProp.Name = "BuisnessID";
directorProp.Type = new CodeTypeReference(typeof(int));
directorProp.Attributes = MemberAttributes.Public | MemberAttributes.Final;
directorClass.Members.Add(directorProp);

CodeMemberMethod overrMethod = new CodeMemberMethod();
overrMethod.Name = "Print";
overrMethod.ReturnType = new CodeTypeReference();
overrMethod.Attributes = MemberAttributes.Public | MemberAttributes.Override;
directorClass.Members.Add(overrMethod);

CodeMemberMethod workMethod = new CodeMemberMethod();
workMethod.Name = "Setup";
workMethod.ReturnType = new CodeTypeReference();
workMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
workerClass.Members.Add(workMethod);