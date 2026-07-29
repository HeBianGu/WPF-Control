// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.Extensions;

public static class MatrixDataExtensions
{
    public static (bool success, string message) ValidateCalibrationMatrix(this Mat matrix, string name)
    {
        if (matrix == null || matrix.IsDisposed || matrix.Empty())
            return (false, $"{name}尚未生成，请先完成相机标定");
        if (matrix.Channels() != 1)
            return (false, $"{name}必须是单通道矩阵");
        return (true, string.Empty);
    }
    public static MatrixData ToMatrixData(this Mat matrix)
    {
        using Mat doubleMatrix = new Mat();
        matrix.ConvertTo(doubleMatrix, MatType.CV_64FC1);

        double[] values = new double[checked(doubleMatrix.Rows * doubleMatrix.Cols)];
        for (int row = 0; row < doubleMatrix.Rows; row++)
        {
            for (int column = 0; column < doubleMatrix.Cols; column++)
                values[row * doubleMatrix.Cols + column] = doubleMatrix.At<double>(row, column);
        }
        return new MatrixData
        {
            Rows = doubleMatrix.Rows,
            Columns = doubleMatrix.Cols,
            Data = values
        };
    }
    public static Mat ToMat(this MatrixData matrixData, string matrixName)
    {
        if (matrixData == null)
            throw new InvalidDataException($"标定文件缺少{matrixName}");
        if (matrixData.Rows <= 0 || matrixData.Columns <= 0)
            throw new InvalidDataException($"{matrixName}的行数或列数无效");
        if (!string.Equals(matrixData.DataType, "CV_64FC1", StringComparison.OrdinalIgnoreCase))
            throw new NotSupportedException($"不支持{matrixName}的数据类型：{matrixData.DataType}");
        if (matrixData.Data == null)
            throw new InvalidDataException($"{matrixName}缺少矩阵数据");

        int expectedLength;
        try
        {
            expectedLength = checked(matrixData.Rows * matrixData.Columns);
        }
        catch (OverflowException ex)
        {
            throw new InvalidDataException($"{matrixName}的矩阵尺寸过大", ex);
        }

        if (matrixData.Data.Length != expectedLength)
            throw new InvalidDataException($"{matrixName}的数据数量与矩阵尺寸不一致");
        if (matrixData.Data.Any(value => !double.IsFinite(value)))
            throw new InvalidDataException($"{matrixName}包含非有限数值");

        using Mat values = Mat.FromArray(matrixData.Data);
        using Mat reshaped = values.Reshape(1, matrixData.Rows);
        Mat result = reshaped.Clone();
        if (result.Rows != matrixData.Rows || result.Cols != matrixData.Columns || result.Type() != MatType.CV_64FC1)
        {
            result.Dispose();
            throw new InvalidDataException($"无法按指定尺寸还原{matrixName}");
        }
        return result;
    }
}
public sealed class MatrixData
{
    public int Rows { get; init; }
    public int Columns { get; init; }
    public string DataType { get; init; } = "CV_64FC1";
    public double[] Data { get; init; }
}

