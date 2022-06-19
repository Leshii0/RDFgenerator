grammar Gramm;

program: line* EOF;

line: statement | ifBlock | whileBlock;

statement: (assingment | functionCall) ';';

ifBlock: 'if' '(' expression ')' block ('else' elseIfBlock)?;

elseIfBlock: block | ifBlock;

whileBlock: 'while' '(' expression ')' block;

assingment
	: IDENTIFIER '=' expression 
	| IDENTIFIER IDENTIFIER '=' newClass 
	| propMember '=' expression 
	| propMember '=' enumeration 
	| propMember '.' functionCall
	| propMember '=' newClass
	;
propMember: IDENTIFIER '.' IDENTIFIER;
class_constructor: IDENTIFIER '(' (expression (',' expression)*)? ')' | IDENTIFIER '(' ('typeof' '(' TYPE ')')? ')';
typeDeclaration: TYPE | 'typeof' '(' TYPE ')';
enumeration: expression (('|' expression)*)?;
functionCall: IDENTIFIER '(' (expression (',' expression)*)? ')';

newClass: NEW class_constructor;

expression
	: constant								#constantExpression
	| IDENTIFIER							#identifierExpression
	| propMember							#propertyExpression
	| functionCall							#functionCallExpression
	| '(' expression ')'					#bracketsExpression
	| '!' expression						#notExpression
	| expression multOp expression			#multiExpression
	| expression addOp expression			#addExpression
	| expression compareOp expression		#compareExpression
	| expression boolOp expression			#boolExpression
	;

multOp: '*' | '/' ;
addOp: '+' | '-';
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=';
boolOp: BOOL_OPERATOR;

BOOL_OPERATOR: '&' | '|' ;

constant: INTEGER | DOUBLE | STRING | BOOL | NULL ;

INTEGER: [0-9]+;
DOUBLE: [0-9]+ '.' [0-9]+;
STRING: ('"' ~'"'* '"') ;
BOOL: 'true' | 'false';
NULL: 'null';
NEW: 'new';
TYPE
	: 'string'							
	| 'int'								
	| 'double'							
	| 'bool'							
	;
block: '{' line* '}';

WS: [ \t\r\n]+ -> skip;
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;