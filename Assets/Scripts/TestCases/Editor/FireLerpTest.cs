using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class FireLerpTest : MonoBehaviour {

    TankFire tf;

    public TankFire CreateObj()
    {
        GameObject obj = new GameObject();
        TankFire tankFire = obj.AddComponent<TankFire>();
        return tankFire;
    }

    public bool testUnclamped(Vector3 a, Vector3 b, float t)
    {
        Vector3 vec1 = Vector3.LerpUnclamped(a, b, t);
        Vector3 vec2 = tf.VectorLerp(a, b, t);

        if (vec1 == vec2)
        {
            return true;
        }

        return false;
    }

    [Test]
	public void LerpStraightTest()
    {
        Vector3 a = new Vector3(0, 0, 0);
        Vector3 b = new Vector3(100, 0, 0);

        tf = CreateObj();
        
        Vector3 c = Vector3.LerpUnclamped(a, b, 0.5f);
        Vector3 d = Vector3.Lerp(a, b, 0.5f);
        Vector3 e = tf.VectorLerp(a, b, 0.5f);

        Assert.IsTrue(c == d);
        Assert.IsTrue(d == e);

        Vector3 vec1 = Vector3.LerpUnclamped(a, b, 1.5f);
        Vector3 vec2 = tf.VectorLerp(a, b, 1.5f);

        Assert.IsTrue(vec1 == vec2);

        for (int i=0; i < 10; i++)
        {
            Assert.IsTrue(testUnclamped(a, b, i));
        }
        
    }

    [Test]
    public void LerpAcrossTest()
    {
        Vector3 vec1 = new Vector3(15, 0, 10);
        Vector3 vec2 = new Vector3(27, 0, 7);

        tf = CreateObj();

        for (int i=0; i < 20; i++)
        {
            Assert.IsTrue(testUnclamped(vec1, vec2, i));
        }
    }

    [Test]
    public void LerpMaxDistance()
    {
        float maxDistance = 30.0f;

        TankFire tf = CreateObj();
    }
}
