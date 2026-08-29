import { Document, Page, StyleSheet, Text, View } from "@react-pdf/renderer";
import { RecordFlat } from "@/data/interfaces/RecordFlat";
import { FieldComposition } from "@/data/interfaces/FieldComposition";

export interface ConversionPdfData {
  haberes: number;
  retenciones: number;
  neto: number;
  values: Array<FieldComposition>;
  source: RecordFlat;
  target: RecordFlat;
}

const styles = StyleSheet.create({
  page: {
    padding: 32,
    fontSize: 10,
    fontFamily: "Helvetica",
    color: "#1a1a1a",
  },
  title: {
    fontSize: 16,
    fontWeight: 700,
    marginBottom: 16,
  },
  section: {
    marginBottom: 20,
  },
  sectionTitle: {
    fontSize: 13,
    fontWeight: 700,
    marginBottom: 8,
    borderBottom: "1pt solid #cccccc",
    paddingBottom: 4,
  },
  row: {
    flexDirection: "row",
    gap: 24,
  },
  headerBox: {
    flex: 1,
    border: "1pt solid #dddddd",
    borderRadius: 4,
    padding: 8,
  },
  headerBoxTitle: {
    fontWeight: 700,
    marginBottom: 4,
    textAlign: "center",
  },
  fieldRow: {
    flexDirection: "row",
    justifyContent: "space-between",
    paddingVertical: 2,
  },
  fieldLabel: {
    color: "#555555",
  },
  fieldValue: {
    fontWeight: 700,
  },
  totalsTable: {
    border: "1pt solid #dddddd",
    borderRadius: 4,
    width: 260,
  },
  totalsRow: {
    flexDirection: "row",
    justifyContent: "space-between",
    paddingVertical: 4,
    paddingHorizontal: 8,
    borderBottom: "1pt solid #eeeeee",
  },
  compositionBlock: {
    marginBottom: 12,
    border: "1pt solid #dddddd",
    borderRadius: 4,
    padding: 8,
  },
  compositionHeader: {
    flexDirection: "row",
    justifyContent: "space-between",
    marginBottom: 4,
  },
  compositionLabel: {
    fontWeight: 700,
  },
  compositionValue: {
    fontWeight: 700,
  },
  compositionSubtitle: {
    fontSize: 8,
    color: "#777777",
    marginBottom: 4,
  },
  sourceFieldRow: {
    flexDirection: "row",
    justifyContent: "space-between",
    backgroundColor: "#f5f5f6",
    borderRadius: 3,
    paddingVertical: 3,
    paddingHorizontal: 6,
    marginBottom: 2,
  },
});

function HeaderBox({
  header,
  record,
}: {
  header: string;
  record: RecordFlat;
}) {
  return (
    <View style={styles.headerBox}>
      <Text style={styles.headerBoxTitle}>{header}</Text>
      <View style={styles.fieldRow}>
        <Text style={styles.fieldLabel}>Template</Text>
        <Text style={styles.fieldValue}>{record.name}</Text>
      </View>
      <View style={styles.fieldRow}>
        <Text style={styles.fieldLabel}>Título</Text>
        <Text style={styles.fieldValue}>{record.title}</Text>
      </View>
      <View style={styles.fieldRow}>
        <Text style={styles.fieldLabel}>Descripción</Text>
        <Text style={styles.fieldValue}>{record.description}</Text>
      </View>
    </View>
  );
}

export function ConversionPdfDocument({ data }: { data: ConversionPdfData }) {
  return (
    <Document>
      <Page size="A4" style={styles.page}>
        <Text style={styles.title}>Conversión</Text>

        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Detalles</Text>
          <View style={styles.row}>
            <HeaderBox header="Origen" record={data.source} />
            <HeaderBox header="Destino" record={data.target} />
          </View>
        </View>

        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Conversión</Text>
          <View style={styles.totalsTable}>
            <View style={styles.totalsRow}>
              <Text>Haberes</Text>
              <Text>{data.haberes}</Text>
            </View>
            <View style={styles.totalsRow}>
              <Text>Retenciones</Text>
              <Text>{data.retenciones}</Text>
            </View>
            <View style={[styles.totalsRow, { borderBottom: "none" }]}>
              <Text>Neto</Text>
              <Text>{data.neto}</Text>
            </View>
          </View>
        </View>

        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Composición</Text>
          {data.values.map((composition) => (
            <View key={composition.fieldId} style={styles.compositionBlock} wrap={false}>
              <View style={styles.compositionHeader}>
                <Text style={styles.compositionLabel}>{composition.label}</Text>
                <Text style={styles.compositionValue}>{composition.value}</Text>
              </View>
              <Text style={styles.compositionSubtitle}>Compuesto por</Text>
              {composition.sourceFields.map((sourceField) => (
                <View key={sourceField.fieldId} style={styles.sourceFieldRow}>
                  <Text>{sourceField.label}</Text>
                  <Text>{sourceField.value}</Text>
                </View>
              ))}
            </View>
          ))}
        </View>
      </Page>
    </Document>
  );
}
