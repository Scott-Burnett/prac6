// Handle cross reference table for Parva
// P.D. Terry, Rhodes University, 2016

using Library;
using System.Collections.Generic;

namespace Parva {

  class Entry {                      // Cross reference table entries
    public string name;              // The identifier itself
    public List<int> refs;           // Line numbers where it appears
    public Entry(string name) {
      this.name = name;
      this.refs = new List<int>(){1};
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

    public static void AddRef(string name, bool declared, int lineRef) {
    // Enters name if not already there, and then adds another line reference (negative
    // if at a declaration point in the original source program)
        int pos = -1, i = 0;
        foreach (Entry entry in entryList) {
            if (entry.name == name) {
                pos = i;
                break;
            }
            i++;
        }
        if (pos == -1) {
            Entry newEntry = new Entry(name);
            if (declared)
                newEntry.refs[0] = lineRef * -1;
            else
                newEntry.refs.Add(lineRef);
            entryList.Add(newEntry);
        }
        else {
            if (declared)
                entryList[pos].refs[0] = lineRef * -1;
            else
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
