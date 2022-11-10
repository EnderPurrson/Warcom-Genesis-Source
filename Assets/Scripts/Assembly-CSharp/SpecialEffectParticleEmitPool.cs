using UnityEngine;

public class SpecialEffectParticleEmitPool : MonoBehaviour
{
	private SpecialEffectParticleEmit[] m_pool;

	private int m_index;

	public void Awake()
	{
		m_pool = base.gameObject.GetComponentsInChildren<SpecialEffectParticleEmit>(true);
		m_index = 0;
	}

	public void Emit()
	{
		if (m_pool.Length > 0)
		{
			m_pool[m_index % m_pool.Length].Emit();
		}
		m_index++;
	}
}
