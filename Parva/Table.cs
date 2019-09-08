// Handle cross reference table for Parva
// P.D. Terry, Rhodes University, 2016

using Library;
using System;
using System.Collections.Generic;

namespace Parva {

const int 
consKind = 0,
varKind = 1,
funcKind = 2;

const int
noType = 0,
intType = 2,
charType = 4,
boolType = 6;

  class Entry {                      // Cross reference table entries
    public string name;             // The identifier itself
    public int kind;                // Identifier kind
    public int type;                // Identifier type
    public int value;               // Identifier value
    public List<int> refs;          // Line numbers where it appears
    public Entry(string name, int kind, int type, int lineRef) {
      this.name = name;
      this.kind = kind;
      this.type = type;
      this.value = 0;
      this.refs = new List<int>(){lineRef * -1};
    }
    public Entry(string name, int kind, int type, int lineRef, int value) {
      this.name = name;
      this.kind = kind;
      this.type = type;
      this.value = 0;
      this.refs = new List<int>(){lineRef * -1};
      this.value = value;
    }
    public override string ToString(){
        string s = name + ":    ";
        foreach (int i in refs)
            s += i + ",  ";
        return s;
    }
  } // Entry

  class Table {
    static List<Entry> entryList = new List<Entry>();

    public static void ClearTable() {
    // Clears cross-reference table
        entryList.Clear();
    } // Table.ClearTable

    public static int indexOf(string name) {
    // Return index of Entry matching string name, return -1 if none exists
        int i = 0;
        foreach (Entry entry in entryList)
            if (entry.name == name) {
                return i;
            i++;
        return -1;
    } // indexOf

    public static int valueOf(string name) {
    // Return value of Entry matching string name, return 0 if none exists
        int pos = indexOf (name);
        if (pos == -1) return 0;
        else return entryList[pos].value;
    } // valueOf

    public static int kindOf(string name) {
    // Return kind of Entry matching string name, return 0 if none exists
        int pos = indexOf (name);
        if (pos == -1) return 0;
        else return entryList[pos].kind;
    } // kindOf

    public static int typeOf(string name) {
    // Return type of Entry matching string name, return 0 if none exists
        int pos = indexOf (name);
        if (pos == -1) return 0;
        else return entryList[pos].type;
    } // typeOf

    public static void declare (string name, int kind, int type, int lineRef) {
        if (indexOf(name) != -1) { 
            Console.WriteLine("Error: identifier " + name + " already exists");
            break;
        }
        entryList.Add(new Entry(name, kind, type, lineRef));
    }

    public static void declare (string name, int kind, int type, int lineRef, int value) {
        if (indexOf(name) != -1) { 
            Console.WriteLine("Error: identifier " + name + " already exists");
            break;
        }
        entryList.Add(new Entry(name, kind, type, lineRef, value));
    }

    public static void AddRef(string name, bool declared, int lineRef) {
    // Enters name if not already there, and then adds another line reference (negative
    // if at a declaration point in the original source program)
        int pos = indexOf(name);
        if (pos == -1) {
            Entry newEntry = new Entry(name);
            if (declared)
                newEntry.refs[0] = lineRef * -1;
            else
                newEntry.refs.Add(lineRef);
            entryList.Add(newEntry);
        }
        else {
            entryList[pos].refs.Add(lineRef);
        }
    } // Table.AddRef

    public static void PrintTable() {
    // Prints out all references in the table (eliminate duplicates line numbers)
        foreach (Entry entry in entryList)
            Console.WriteLine(entry.ToString());
    } // Table.PrintTable

  } // Table

} // namespace
