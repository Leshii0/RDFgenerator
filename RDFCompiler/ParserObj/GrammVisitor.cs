using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RDFCompiler.ParserObj
{
    public class SimpleVisitor : GrammBaseVisitor<object?>
    {
        Dictionary<string, object?> Variables = new Dictionary<string, object?>();
        public List<string> examplarDefinitions = new List<string>();
        public List<string> propList = new List<string>();
        public List<string> funcList = new List<string>();
        Dictionary<string, string> classNames = new Dictionary<string, string>();
        public Dictionary<string, Property> properties = new Dictionary<string, Property>();
        List<string> enumExamplarNames = new List<string>();
        List<string> enums = new List<string> { "Attributes" };
        Assembly codedomAssembly = Assembly.GetAssembly(typeof(CodeTypeDeclaration));
        string codedomPrefix = "System.CodeDom.";
        Dictionary<string, string> equalNames = new Dictionary<string, string>
        {
            {"ReturnType","Type" }
        };

        public string GetAllData()
        {
            string result = "";
            foreach (var str in examplarDefinitions)
            {
                result += str + Environment.NewLine;
            }
            foreach (var str in properties)
            {
                result += str.Value.PropertyRDF + Environment.NewLine;
            }
            foreach (var str in propList)
            {
                result += str + Environment.NewLine;
            }
            foreach (var str in funcList)
            {
                result += str + Environment.NewLine;
            }
            return result;
            //Console.ResetColor();
            //QuerryProcessor _proc = new QuerryProcessor("CodeDomDB");
            //List<string> data = _proc.GetAll();
            //Console.ForegroundColor = ConsoleColor.Red;
            //foreach (var str in data)
            //{
            //    Console.WriteLine(str);
            //}
            //Console.ResetColor();
            //Console.ReadKey();
        }
        public override object VisitAssingment(GrammParser.AssingmentContext context)
        {
            if (context.propMember() != null)
            {
                if (context.functionCall() != null)
                {
                    object[] function = (object[])Visit(context.functionCall());
                    string funcName = function[0].ToString();
                    var propName = context.propMember().IDENTIFIER(1).GetText();
                    if (!properties.ContainsKey(propName + funcName))
                    {
                        properties.Add(propName + funcName, new Property(propName + funcName, ePropTypes.function));



                    }
                    var paramCount = CountEven(context.functionCall().ChildCount - 3);
                    for (int i = 0; i < paramCount; i++)
                    {
                        var expr = context.functionCall().expression(i);

                        var val = classNames.ContainsKey(Visit(expr).ToString()) ? ":" + classNames[Visit(expr).ToString()] : GetRdfType(Visit(expr).GetType(), Visit(expr).ToString());
                        if (!properties[propName + funcName].Range.Contains(val))
                            properties[propName + funcName].Range.Add(val);
                    }
                    var dom = ":" + classNames[context.propMember().IDENTIFIER(0).GetText()];
                    if (!properties[propName + funcName].Domain.Contains(dom))
                        properties[propName + funcName].Domain.Add(dom);
                    for (int i = 0; i < paramCount; i++)
                    {
                        var subject = Visit(context.functionCall().expression(i)).ToString();
                        var funcString = ":" + context.propMember().IDENTIFIER(0).GetText() + " "
                            + properties[propName + funcName].Header
                            + (classNames.ContainsKey(subject) ? " :" : " ") + subject + ".";
                        if (!funcList.Contains(funcString))
                            funcList.Add(funcString);
                    }

                }
                else if (context.enumeration() != null)
                {
                    string[] value = (string[])Visit(context.enumeration());
                    var propName = context.propMember().IDENTIFIER(1).GetText();
                    if (!properties.ContainsKey(propName))
                    {
                        properties.Add(propName, new Property(propName, ePropTypes.enumeration));
                        //var valueType = Visit(context.expression()).GetType();

                        properties[propName].Domain.Add(":" + classNames[context.propMember().IDENTIFIER(0).GetText()]);
                        //properties[propName].Range.Add(GetRdfType(valueType));

                    }
                    var enumName = GenerateEnumName(propName, value);
                    for (int i = 0; i < value.Length; i++)
                    {
                        var enumer = ":" + enumName + " rdf:_" + (i + 1).ToString() + " :" + value[i] + ".";
                        if (!propList.Contains(enumer))
                            propList.Add(enumer);
                    }

                }
                else if (context.expression() != null)
                {
                    var propName = context.propMember().IDENTIFIER(1).GetText();
                    var value = Visit(context.expression());
                    if (!properties.ContainsKey(propName))
                    {
                        properties.Add(propName, new Property(propName, enums.Contains(propName) ? ePropTypes.enumeration : ePropTypes.property));


                    }
                    var valueType = Visit(context.expression()).GetType();
                    var val = GetRdfType(valueType, Visit(context.expression()).ToString());
                    var dom = ":" + classNames[context.propMember().IDENTIFIER(0).GetText()];
                    if (!properties[propName].Domain.Contains(dom))
                        properties[propName].Domain.Add(dom);
                    if (!properties[propName].Range.Contains(val))
                        properties[propName].Range.Add(val);
                    if (!enums.Contains(propName))
                        propList.Add(":" + context.propMember().IDENTIFIER(0).GetText() + " " + properties[propName].Header + " " + value + ".");
                    else
                    {
                        var enumName = GenerateEnumName(propName, value.ToString());
                        propList.Add(":" + enumName + " rdf:_1" + " :" + value + ".");
                    }

                }
                else if (context.newClass() != null)
                {
                    var propName = context.propMember().IDENTIFIER(1).GetText();
                    var className = context.newClass().class_constructor().IDENTIFIER().GetText();
                    if (className == "CodeTypeReference")
                    {
                        var changedName = equalNames.ContainsKey(propName) ? equalNames[propName] : propName;
                        if (!properties.ContainsKey(changedName))
                            properties.Add(changedName, new Property(propName, ePropTypes.property));
                        var dom = ":" + classNames[context.propMember().IDENTIFIER(0).GetText()];
                        if (!properties[changedName].Domain.Contains(dom))
                            properties[changedName].Domain.Add(dom);
                        //var paramCount = CountEven(context.newClass().class_constructor().ChildCount - 3);


                        if (context.newClass().class_constructor().TYPE() != null)
                        {
                            var val = "xsd:" + context.newClass().class_constructor().TYPE().GetText();
                            if (!properties[changedName].Range.Contains(val))
                                properties[changedName].Range.Add(val);

                            var subject = context.newClass().class_constructor().TYPE().GetText();
                            var funcString = ":" + context.propMember().IDENTIFIER(0).GetText() + " "
                                + properties[changedName].Header + " xsd:" + subject + ".";

                            if (!propList.Contains(funcString))
                                propList.Add(funcString);
                        }
                        else
                        {
                            var val = ":Void";
                            if (!properties[changedName].Range.Contains(val))
                                properties[changedName].Range.Add(val);
                            //var subject = context.newClass().class_constructor().TYPE().GetText();
                            var funcString = ":" + context.propMember().IDENTIFIER(0).GetText() + " "
                                + properties[changedName].Header + " :Void.";

                            if (!propList.Contains(funcString))
                                propList.Add(funcString);
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }


            else if (context.newClass() != null)
            {
                //var textType = codedomPrefix + context.newClass().class_constructor().IDENTIFIER().ToString();
                //Type type =codedomAssembly.GetType(textType);
                //var baseType = GetBaseType(type);
                classNames.Add(context.IDENTIFIER(1).GetText(), context.IDENTIFIER(0).GetText());
                examplarDefinitions.Add(":" + context.IDENTIFIER(1).GetText() + " rdf:type :" + context.IDENTIFIER(0).GetText() + ".");
                var explrClass = context.IDENTIFIER(1).GetText();
                Variables.Add(explrClass, context.IDENTIFIER(1));
            }
            //var name = context.IDENTIFIER(0).GetText();

            //var value = Visit(context.expression());

            //dict[name] = value;
            return null;
        }

        Type? GetBaseType(Type currentType)
        {
            if (currentType.BaseType != null && currentType.BaseType != typeof(System.Object))
            {
                var baseType = GetBaseType(currentType.BaseType);
                return baseType == null ? currentType.BaseType : baseType;
            }
            else return null;
        }

        public override object? VisitFunctionCall(GrammParser.FunctionCallContext context)
        {
            var funcName = context.IDENTIFIER().ToString();

            int count = CountEven(context.ChildCount - 3);
            object[] parameters = new object[count + 1];
            parameters[0] = funcName;
            for (int i = 0; i < count; i++)
            {
                parameters[i + 1] = Visit(context.expression(i));
            }

            return parameters;
        }
        public string GenerateEnumName(string header, params string[] values)
        {
            string prefix = "";
            foreach (var str in values)
            {
                prefix += str.Substring(0, 1);
            }
            string nameEnum = prefix + "_" + header;
            if (enumExamplarNames.Contains(nameEnum))
                return nameEnum;
            else
            {
                enumExamplarNames.Add(nameEnum);
                return nameEnum;
            }

        }

        int CountEven(int number)
        {
            int count = 0;
            for (int i = 0; i < number; i++)
            {
                if (i % 2 == 0)
                    count++;
            }
            return count;
        }
        public override object? VisitEnumeration(GrammParser.EnumerationContext context)
        {

            var count = CountEven(context.ChildCount);
            string[] values = new string[count];
            for (int i = 0; i < count; i++)
            {
                values[i] = Visit(context.expression(i)).ToString();
            }


            return values;
        }
        public string GetRdfType(Type type, string textType)
        {

            if (type == typeof(string))
            {
                return "xsd:string";
            }
            else if (type == typeof(int))
            {
                return "xsd:int";
            }

            return type.ToString();

        }
        public override object? VisitNewClass(GrammParser.NewClassContext context)
        {
            //if(context.class_constructor().IDENTIFIER() != null)
            //{
            //    if (classNames.Contains(context.class_constructor().IDENTIFIER().GetText()))
            //    {
            //        examplarDefinitions.Add(":"+context)
            //    }
            //}
            return new NotImplementedException();
        }

        public override object? VisitPropMember(GrammParser.PropMemberContext context)
        {
            var propName = context.IDENTIFIER(1).GetText();

            //if (!properties.ContainsKey(propName))
            //{
            //    properties.Add(propName, new Property(propName, enums.Contains(propName) ? ePropTypes.enumeration : ePropTypes.property));
            //}


            return propName;
        }
        public override object? VisitConstant(GrammParser.ConstantContext context)
        {
            if (context.INTEGER() is { } i)
                return int.Parse(i.GetText());

            if (context.DOUBLE() is { } d)
                return double.Parse(d.GetText());

            if (context.STRING() is { } s)
                return s.GetText()/*[1..^1]*/;

            if (context.BOOL() is { } b)
                return bool.Parse(b.GetText());

            if (context.NULL() is { } n)
                return null;

            return new NotImplementedException();
        }

        public override object? VisitIdentifierExpression(GrammParser.IdentifierExpressionContext context)
        {
            var varName = context.IDENTIFIER().GetText();

            if (!Variables.ContainsKey(varName))
            {

            }

            return Variables[varName];
        }

        public override object? VisitAddExpression(GrammParser.AddExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.addOp().GetText();

            return op switch
            {
                "+" => Add(left, right),
                "-" => Subtract(left, right),
                _ => throw new NotImplementedException()
            };
        }

        public override object? VisitMultiExpression(GrammParser.MultiExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.multOp().GetText();

            return op switch
            {
                "*" => Mult(left, right),
                "-" => Divide(left, right),
                _ => throw new NotImplementedException()
            };
        }

        private object Mult(object left, object right)
        {
            throw new NotImplementedException();
        }

        private object Divide(object left, object right)
        {
            throw new NotImplementedException();
        }

        private object Subtract(object left, object right)
        {
            throw new NotImplementedException();
        }

        private object Add(object left, object right)
        {
            if (left is int l && right is int r)
                return l + r;

            if (left is double ld && right is double rd)
                return ld + rd;

            if (left is int lInt && right is double rDo)
                return lInt + rDo;

            if (left is double lDoub && right is int rInt)
                return lDoub + rInt;

            if (left is string)
                return $"{left}{right}";

            if (right is string)
                return $"{left}{right}";

            throw new NotImplementedException();
        }
    }
}
